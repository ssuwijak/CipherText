using System.Security.Cryptography;
using System.Text;

namespace CipherText
{
    public class HashDemo
    {
        public static void Run(string data)
        {
            string[] hash = { ComputeHash(data), ComputeSHA256(data), ComputeSHA512(data) };

            Console.WriteLine($"data={data}\tlength={data.Length}");
            Console.WriteLine();

            Console.WriteLine($"hash={hash[0]}\tlength={hash[0].Length}");
            Console.WriteLine();
            
            Console.WriteLine($"sha256={hash[1]}\tlength={hash[1].Length}");
            Console.WriteLine(HexNumber.BytesToTableString(HexNumber.StringToBytes(hash[1]), 16, " ", false));
            Console.WriteLine();

            Console.WriteLine($"sha512={hash[2]}\tlength={hash[2].Length}");
            Console.WriteLine(HexNumber.BytesToTableString(HexNumber.StringToBytes(hash[2]), 16));
            Console.WriteLine();

            //Console.WriteLine($"utf8 ? {EncodingDemo.IsUtf8(hash[0])}");
            //Console.WriteLine($"utf16 ? {EncodingDemo.IsUtf16(hash[0])}");
        }
        public static string ComputeHash(string message)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
            return HexNumber.BytesToString(hashedBytes);
            //return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
        }

        public static string ComputeSHA256(string message)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
            return HexNumber.BytesToString(bytes);
        }

        public static string ComputeSHA512(string message)
        {
            using var sha512 = SHA512.Create();
            byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(message));
            return HexNumber.BytesToString(bytes);
        }


    }
}
