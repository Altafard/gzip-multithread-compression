using GZipTest.Abstractions;
using System;

namespace GZipTest.Factories
{
    public abstract class FactoryBase : IFactory
    {
        protected readonly string SourceFilePath;
        protected readonly string DestinationFilePath;

        protected FactoryBase(string sourceFilePath, string destinationFilePath)
        {
            if (sourceFilePath == null) throw new ArgumentNullException("sourceFilePath");
            if (destinationFilePath == null) throw new ArgumentNullException("destinationFilePath");

            SourceFilePath = sourceFilePath;
            DestinationFilePath = destinationFilePath;
        }

        public abstract IFileReader CreateReader();

        public abstract IFileWriter CreateWriter();
    }
}
