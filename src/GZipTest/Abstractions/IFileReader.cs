namespace GZipTest.Abstractions
{
    /// <summary>
    /// Input file stream wrapper.
    /// </summary>
    public interface IFileReader : System.IDisposable
    {
        /// <summary>
        /// Read from file stream a block of data.
        /// </summary>
        Block ReadBlock();
    }
}
