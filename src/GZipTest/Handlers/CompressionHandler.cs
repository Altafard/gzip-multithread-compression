using GZipTest.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace GZipTest.Handlers
{
    public class CompressionHandler : GZipHandler
    {
        public CompressionHandler(IFactory factory) : base(factory) { }

        /// <summary>
        /// Compress.
        /// </summary>
        protected override void Process(object number)
        {
            var threadNumber = (int) number;

            Console.WriteLine("Thread {0} started compressing", threadNumber);

            Block block;
            while ((block = Reader.ReadBlock()) != null)
            {
                Debug.Print("Thread {0} process {1} block", threadNumber, block.ID);
                
                using (var ms = new MemoryStream())
                {
                    using (var gzs = new GZipStream(ms, CompressionMode.Compress))
                    {
                        gzs.Write(block.Bytes, 0, block.Bytes.Length);
                    }
                    
                    Writer.WriteBlock(new Block(block.ID, ms.ToArray()));
                }
            }

            Events[threadNumber].Set();
        }
    }
}
