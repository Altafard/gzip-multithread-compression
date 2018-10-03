namespace GZipTest
{
    public class Block
    {
        public Block(int id, byte[] bytes)
        {
            ID = id;
            Bytes = bytes;
        }

        public int ID { get; private set; }

        public byte[] Bytes { get; private set; }
    }
}
