namespace GZipTest.Abstractions
{
    /// <summary>
    /// Output file stream wrapper.
    /// </summary>
    public interface IFileWriter : System.IDisposable
    {
        /// <summary>
        /// Write to file stream a block of data.
        /// </summary>
        /// <param name="block">Numbered block</param>
        void WriteBlock(Block block);
    }
}
