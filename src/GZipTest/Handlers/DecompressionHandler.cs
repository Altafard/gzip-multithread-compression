using GZipTest.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Extensions;

namespace GZipTest.Handlers
{
    public class DecompressionHandler : GZipHandler
    {
        public DecompressionHandler(IFactory factory) : base(factory) { }

        /// <summary>
        /// Decompress.
        /// </summary>
        protected override void Process(object number)
        {
            var threadNumber = (int) number;

            Console.WriteLine("Thread {0} started decompressing", threadNumber);

            Block block;
            while ((block = Reader.ReadBlock()) != null)
            {
                Debug.Print("Thread {0} process {1} block", threadNumber, block.ID);
                
                using (var msIn = new MemoryStream(block.Bytes))
                {
                    using (var gzs = new GZipStream(msIn, CompressionMode.Decompress))
                    {
                        using (var msOut = new MemoryStream())
                        {
                            gzs.CopyTo(msOut);
                            Writer.WriteBlock(new Block(block.ID, msOut.ToArray()));
                        }
                    }
                }
            }

            Events[threadNumber].Set();
        }
    }
}
