using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Security.Cryptography;
using System.Text;

namespace InventoryOrderingSystem.Helpers
{
    public class SecurityHelper
    {
        //Add salt to password and hash it

        //Password Hashing - Using PBKDF2 (Password-Based Key Derivation Function 2)

        //Constant (salt size and hash size)

        private static int saltSize = 16; //128 bits
        private const int hashSize = 32; //256 bits
        private const int iteration = 10000;//Minimum requirement for iteration but the standard is 100000 iterations
        private static byte[] encryptionKey = Encoding.UTF8.GetBytes("TiTE6&67^@ASDFghjlqwerty12345678"); // Must be 32 bytes for AES-256

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: iteration,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: hashSize
                );
            byte[] hashBytes = new byte[saltSize + hashSize];

            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(enteredPassword);
            byte[] salt = new byte[saltSize];
            Array.Copy(hashBytes, 0, salt, 0, saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                iterations: iteration,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: hashSize
                );
            return CryptographicOperations.FixedTimeEquals(
                hash, hashBytes.AsSpan(saltSize, hashSize)

                );

        }
        //Add Encryption for email
        public static string EncryptionEmail(string email)
        {
            using Aes aes = Aes.Create();
            aes.Key = encryptionKey;
            aes.GenerateIV();

            using MemoryStream ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var encryptor = aes.CreateEncryptor())
            using (var crypto = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(crypto))
            {
                sw.Write(email);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptEmail(string encryptedEmail)
        {
            byte[] cipher = Convert.FromBase64String(encryptedEmail);
            using Aes aes = Aes.Create();
            aes.Key = encryptionKey;
            byte[] iv = new byte[16];
            Array.Copy(cipher, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using MemoryStream ms = new MemoryStream(
                cipher,
                iv.Length,
                cipher.Length - iv.Length);

            using var decryptor = aes.CreateDecryptor();
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
