using GZipTest.Abstractions;
using System.IO;

namespace GZipTest.Readers
{
    public class FileReader : IFileReader
    {
        private const int DefaultBlockSize = 1024 * 1024; // 1MB

        private readonly Stream _fs;
        private readonly object _syncRoot = new object();

        private int _blockID = 0;

        public FileReader(string filePath)
        {
            _fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
        }

        public Block ReadBlock()
        {
            byte[] buffer;

            lock (_syncRoot)
            {
                if (_fs.Length == _fs.Position)
                {
                    return null;
                }

                int read = _fs.Length - _fs.Position <= DefaultBlockSize
                    ? (int) (_fs.Length - _fs.Position)
                    : DefaultBlockSize;

                buffer = new byte[read];
                _fs.Read(buffer, 0, read);

                _blockID++;

                return new Block(_blockID, buffer);
            }
        }

        public void Dispose()
        {
            _fs.Dispose();
        }
    }
}
