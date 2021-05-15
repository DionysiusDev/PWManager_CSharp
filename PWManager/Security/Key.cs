using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWManager
{
    [Serializable]
    public static class Key
    {
        public static byte[] DbKey { get; set; }

        public static byte[] GetKey()
        {
            return DbKey;
        }
    }
}
