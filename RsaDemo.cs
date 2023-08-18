using System.Security.Cryptography;
using System.Text;


/*
 * https://dev.to/stratiteq/cryptography-with-practical-examples-in-net-core-1mc4
 */

namespace CipherText
{
    public class RsaDemo
    {
        public static void Run(string data)
        {
            Console.WriteLine("***** Asymmetric encryption demo *****");

            var unencryptedMessage = "To be or not to be, that is the question, whether tis nobler in the...";
            unencryptedMessage = data;

            Console.WriteLine("Unencrypted message: " + unencryptedMessage);

            // 1. Create a public / private key pair.
            RSAParameters privateAndPublicKeys, publicKeyOnly;
            using (var rsaAlg = RSA.Create())
            {
                privateAndPublicKeys = rsaAlg.ExportParameters(includePrivateParameters: true);
                publicKeyOnly = rsaAlg.ExportParameters(includePrivateParameters: false);
            }

            // 2. Sender: Encrypt message using public key
            var encryptedMessage = Encrypt(unencryptedMessage, publicKeyOnly);
            Console.WriteLine("Sending encrypted message: " + HexNumber.BytesToString(encryptedMessage));

            // 3. Receiver: Decrypt message using private key
            var decryptedMessage = Decrypt(encryptedMessage, privateAndPublicKeys);
            Console.WriteLine("Recieved and decrypted message: " + decryptedMessage);

            Console.Write(Environment.NewLine);
        }
        public static void MessageSignatureDemo()
        {
            Console.WriteLine("***** Message signature demo *****");

            var message = "To be or not to be, that is the question, whether tis nobler in the...";
            Console.WriteLine("Message to be verified: " + message);

            // 1. Create a public / private key pair.
            RSAParameters privateAndPublicKeys, publicKeyOnly;
            using (var rsaAlg = RSA.Create())
            {
                privateAndPublicKeys = rsaAlg.ExportParameters(includePrivateParameters: true);
                publicKeyOnly = rsaAlg.ExportParameters(includePrivateParameters: false);
            }

            // 2. Sender: Sign message using private key
            var signature = Sign(message, privateAndPublicKeys);
            Console.WriteLine("Message signature: " + HexNumber.BytesToString(signature));

            // 3. Receiver: Verify message authenticity using public key
            var isTampered = Verify(message, signature, publicKeyOnly);
            Console.WriteLine("Message is untampered: " + isTampered.ToString());

            Console.Write(Environment.NewLine);
        }
        public static byte[] Encrypt(string message, RSAParameters rsaParameters)
        {
            using var rsaAlg = RSA.Create(rsaParameters);
            return rsaAlg.Encrypt(Encoding.UTF8.GetBytes(message), RSAEncryptionPadding.Pkcs1);
        }

        public static string Decrypt(byte[] cipherText, RSAParameters rsaParameters)
        {
            using var rsaAlg = RSA.Create(rsaParameters);
            var decryptedMessage = rsaAlg.Decrypt(cipherText, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedMessage);
        }

        public static byte[] Sign(string message, RSAParameters rsaParameters)
        {
            using var rsaAlg = RSA.Create(rsaParameters);
            return rsaAlg.SignData(Encoding.UTF8.GetBytes(message), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static bool Verify(string message, byte[] signature, RSAParameters rsaParameters)
        {
            using var rsaAlg = RSA.Create(rsaParameters);
            return rsaAlg.VerifyData(Encoding.UTF8.GetBytes(message), signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
