using GZipTest.Abstractions;
using System;
using System.Threading;

namespace GZipTest.Handlers
{
    public abstract class GZipHandler : IGZipHandler
    {
        protected readonly IFileReader Reader;
        protected readonly IFileWriter Writer;
        protected readonly ManualResetEvent[] Events;

        protected GZipHandler(IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");

            Reader = factory.CreateReader();
            Writer = factory.CreateWriter();
            Events = new ManualResetEvent[Environment.ProcessorCount];
        }

        public void Handle()
        {
            for (var @event = 0; @event < Events.Length; @event++)
            {
                Events[@event] = new ManualResetEvent(false);

                var thread = new Thread(Process) { IsBackground = false };
                thread.Start(@event);
            }

            WaitHandle.WaitAll(Events);
        }

        /// <summary>
        /// Process a GZip operation..
        /// </summary>
        protected abstract void Process(object number);

        public virtual void Dispose()
        {
            Reader.Dispose();
            Writer.Dispose();
        }
    }
}
