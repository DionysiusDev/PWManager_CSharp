using System;

namespace Security
{
    [Serializable]
    public static class Key
    {
        public static byte[] DbKey { get; set; }

        public static byte[] GetKey()
        {
            return DbKey;
        }

        public static byte[] SetKey(byte[] KeyToSet)
        {
            return DbKey = KeyToSet;
        }
    }
}
