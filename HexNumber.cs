using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherText
{
    public class HexNumber
    {
        public static byte[] BytesEmptyNull() => Encoding.UTF8.GetBytes("");
        public static void Trim(ref string hexString)
        {
            hexString = hexString.Trim().ToUpper();
            hexString = hexString.Replace(" ", "");
            hexString = hexString.Replace("0X", "");
        }
        public static bool IsHexString(string hexString)
        {
            Trim(ref hexString);

            if (hexString.Length == 0) return false;

            bool isHexString = hexString.All(c => char.IsAsciiHexDigit(c));

            return isHexString;
        }
        public static byte[] StringToBytes(string hexString)
        {
            if (!IsHexString(hexString)) return BytesEmptyNull();

            int len = hexString.Length;

            if (len % 2 != 0) hexString = "0" + hexString;

            byte[] bytes = new byte[len / 2];

            for (int i = 0; i < len; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);// System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }
        public static string BytesToString(byte[] bytes, string separator = "", bool upperCase = true)
        {
            if (bytes == null || bytes.Length < 1) return "";

            string hexString = BitConverter.ToString(bytes).Replace("-", separator);

            return upperCase ? hexString : hexString.ToLower();

            //StringBuilder hexString = new StringBuilder();

            //for (int i = 0; i < bytes.Length; i++)
            //    hexString.Append(bytes[i].ToString("X2"));

            //return hexString.ToString();
        }
        public static string BytesToTableString(byte[] bytes, int col = 16, string separator = " ", bool upperCase = true)
        {
            if (col < 1) return BytesToString(bytes, separator, upperCase);
            if (col > 64) col = 16;

            StringBuilder sb = new StringBuilder();
           
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2") + separator);
                j++;

                if (j >= col)
                {
                    j = 0;
                    sb.Append("\n");
                }
            }

            return upperCase ? sb.ToString() : sb.ToString().ToLower();
        }
        internal static bool IsUtf8(string hexString)
        {
            for (int i = 0; i < hexString.Length; i += 2)
            {
                if (int.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber) > 127)
                {
                    return false;
                }
            }

            return true;
        }

        internal static bool IsUtf16(string hexString)
        {
            for (int i = 0; i < hexString.Length; i += 2)
            {
                if (int.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber) > 255)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
