namespace GZipTest.Abstractions
{
    public interface IFactory
    {
        IFileReader CreateReader();

        IFileWriter CreateWriter();
    }
}
