using GZipTest.Abstractions;
using GZipTest.Readers;
using GZipTest.Writers;

namespace GZipTest.Factories
{
    /// <summary>
    /// Factory for building a reader for target file and a writer for compressed file.
    /// </summary>
    public class CompressionFactory : FactoryBase
    {
        public CompressionFactory(string sourceFilePath, string destinationFilePath) : base(sourceFilePath, destinationFilePath) { }

        public override IFileReader CreateReader()
        {
            return new FileReader(SourceFilePath);
        }

        public override IFileWriter CreateWriter()
        {
            return new CompressedFileWriter(DestinationFilePath);
        }
    }
}
