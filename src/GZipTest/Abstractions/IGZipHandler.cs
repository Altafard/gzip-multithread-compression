namespace GZipTest.Abstractions
{
    /// <summary>
    /// GZip-operation runner.
    /// </summary>
    public interface IGZipHandler : System.IDisposable
    {
        /// <summary>
        /// Execute an operation in multi threads (count of threads depend on processor cores count).
        /// </summary>
        void Handle();
    }
}
