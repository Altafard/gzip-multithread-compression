namespace System.IO.Extensions
{
    /// <summary>
    /// This extension added in .NET 4.0
    /// </summary>
    public static class StreamExtensions
    {
        private const int DefaultBufferSize = 4096;

        public static void CopyTo(this Stream source, Stream destination)
        {
            CopyTo(source, destination, DefaultBufferSize);
        }

        public static void CopyTo(this Stream source, Stream destination, int bufferSize)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("destination");

            var buffer = new byte[bufferSize];

            int read;
            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, read);

            destination.Flush();
        }
    }
}
