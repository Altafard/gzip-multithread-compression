using GZipTest.Abstractions;
using System;
using System.IO;

namespace GZipTest.Readers
{
    public class CompressedFileReader : IFileReader
    {
        private readonly Stream _fs;
        private readonly object _syncRoot = new object();

        private int _blockID = 0;

        public CompressedFileReader(string filePath)
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

                byte[] blockLenghtBytes = new byte[sizeof(int)];
                _fs.Read(blockLenghtBytes, 0, blockLenghtBytes.Length);
                int blockLength = BitConverter.ToInt32(blockLenghtBytes, 0);

                buffer = new byte[blockLength];
                _fs.Read(buffer, 0, blockLength);

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
