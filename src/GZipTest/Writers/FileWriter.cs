using GZipTest.Abstractions;
using System.IO;
using System.Threading;

namespace GZipTest.Writers
{
    public class FileWriter : IFileWriter
    {
        private readonly Stream _fs;
        private readonly object _syncRoot = new object();

        private int _blockID = 1;

        public FileWriter(string filePath)
        {
            _fs = File.Open(filePath, FileMode.CreateNew, FileAccess.Write);
        }

        public void WriteBlock(Block block)
        {
            lock (_syncRoot)
            {
                while (_blockID != block.ID)
                    Monitor.Wait(_syncRoot);

                _fs.Write(block.Bytes, 0, block.Bytes.Length);

                _blockID++;
                Monitor.PulseAll(_syncRoot);
            }
        }

        public void Dispose()
        {
            _fs.Dispose();
        }
    }
}
