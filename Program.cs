using System.Security.Cryptography;
using System.Text;

namespace CipherText
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string data = @"สวัสดี Hello !!!";

            //EncodingDemo.Run();
            HashDemo.Run(data);
            //AesDemo.Run(data);
            //RsaDemo.Run(data);
            //RsaDemo.MessageSignatureDemo();
        }
    }
}