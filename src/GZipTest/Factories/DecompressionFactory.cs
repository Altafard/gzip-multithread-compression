using GZipTest.Abstractions;
using GZipTest.Readers;
using GZipTest.Writers;

namespace GZipTest.Factories
{
    /// <summary>
    /// Factory for building a reader for compressed file and a writer for target file.
    /// </summary>
    public class DecompressionFactory : FactoryBase
    {
        public DecompressionFactory(string sourceFilePath, string destinationFilePath) : base(sourceFilePath, destinationFilePath) { }

        public override IFileReader CreateReader()
        {
            return new CompressedFileReader(SourceFilePath);
        }

        public override IFileWriter CreateWriter()
        {
            return new FileWriter(DestinationFilePath);
        }
    }
}
