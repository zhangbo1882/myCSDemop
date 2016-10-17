using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    struct QuoteTypeDef
    {

    }
    class MyConvert
    {
        public static void ByteToByte(byte[] s1, int len1, byte[] s2, int len2)
        {
            int i;
            for (i = 0; i < len2; i++)
            {
                if (i >= len1)
                {
                    break;
                }
                s1[i] = s2[i];
            }
        }

        public static void Char2Byte(char from, out byte to)
        {
            to = Convert.ToByte(from);
        }

        public static void String2ByteArray(string from, out byte[] to, int len)
        {
            byte[] temp;
            int len_from;
            to = new byte[len];
            if (from != null)
            {
                temp = Encoding.Default.GetBytes(from);
                len_from = Encoding.Default.GetByteCount(from);
                ByteToByte(to, len, temp, len_from);
            }
        }

    }
}
