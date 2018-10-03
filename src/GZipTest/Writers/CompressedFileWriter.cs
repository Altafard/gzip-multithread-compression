using GZipTest.Abstractions;
using System;
using System.IO;
using System.Threading;

namespace GZipTest.Writers
{
    public class CompressedFileWriter : IFileWriter
    {
        private readonly Stream _fs;
        private readonly object _syncRoot = new object();

        private int _blockID = 1;

        public CompressedFileWriter(string filePath)
        {
            _fs = File.Open(filePath, FileMode.CreateNew, FileAccess.Write);
        }

        public void WriteBlock(Block block)
        {
            lock (_syncRoot)
            {
                while (_blockID != block.ID)
                    Monitor.Wait(_syncRoot);

                byte[] blockLengthBytes = new byte[sizeof(int)];
                BitConverter.GetBytes(block.Bytes.Length).CopyTo(blockLengthBytes, 0);

                _fs.Write(blockLengthBytes, 0, blockLengthBytes.Length);
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
