using System.IO;

namespace SupportUtils
{
    public class EncryptStream : FileStream
    {
        private const byte KEY = 111;
        private static readonly byte[] _keys = new byte[] { 111, 123, 147, 149 };

        public EncryptStream(string path, FileMode mode) : base(path, mode) { }

        public EncryptStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize,
            bool useAsync) : base(path, mode, access, share, bufferSize, useAsync) { }

        public override void Write(byte[] array, int offset, int count)
        {
            for (int i = 0; i < array.Length; i++)
            {
                byte key = _keys[i % _keys.Length];
                array[i] ^= key;
            }

            base.Write(array, offset, count);
        }

        public override int Read(byte[] array, int offset, int count)
        {
            int index = base.Read(array, offset, count);
            for (int i = 0; i < array.Length; i++)
            {
                byte key = _keys[i % _keys.Length];
                array[i] ^= key;
            }

            return index;
        }
    }
}