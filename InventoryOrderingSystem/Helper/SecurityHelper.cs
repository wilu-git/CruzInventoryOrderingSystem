using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Security.Cryptography;
using System.Text;

namespace InventoryOrderingSystem.Helpers
{
    /// <summary>
    /// Provides cryptographic security utilities for password hashing and email encryption.
    /// Uses PBKDF2 for password hashing and AES-256 for email encryption.
    /// </summary>
    public class SecurityHelper
    {
        //Add salt to password and hash it

        //Password Hashing - Using PBKDF2 (Password-Based Key Derivation Function 2)

        //Constant (salt size and hash size)

        /// <summary>
        /// The size of the salt in bytes (128 bits).
        /// </summary>
        private static int saltSize = 16; //128 bits
        
        /// <summary>
        /// The size of the hash output in bytes (256 bits).
        /// </summary>
        private const int hashSize = 32; //256 bits
        
        /// <summary>
        /// The number of iterations for PBKDF2 key derivation.
        /// Minimum requirement for iteration but the standard is 100000 iterations.
        /// </summary>
        private const int iteration = 10000;//Minimum requirement for iteration but the standard is 100000 iterations
        
        /// <summary>
        /// The encryption key used for AES-256 email encryption. Must be 32 bytes for AES-256.
        /// </summary>
        private static byte[] encryptionKey = Encoding.UTF8.GetBytes("TiTE6&67^@ASDFghjlqwerty12345678"); // Must be 32 bytes for AES-256

        /// <summary>
        /// Hashes a password using PBKDF2 with a randomly generated salt.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>A Base64-encoded string containing the salt and hashed password.</returns>
        /// <remarks>
        /// The returned string contains both the salt and hash, allowing verification without storing the salt separately.
        /// Uses SHA256 as the hashing algorithm with 10000 iterations.
        /// </remarks>
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

        /// <summary>
        /// Verifies that an entered password matches a stored hash.
        /// </summary>
        /// <param name="enteredPassword">The plain text password to verify.</param>
        /// <param name="storedHash">The Base64-encoded hash string from <see cref="HashPassword"/>.</param>
        /// <returns>True if the password matches the hash; otherwise, false.</returns>
        /// <remarks>
        /// Uses constant-time comparison to prevent timing attacks.
        /// Extracts the salt from the stored hash and recomputes the hash for comparison.
        /// </remarks>
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
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
        /// <summary>
        /// Encrypts an email address using AES-256 encryption.
        /// </summary>
        /// <param name="email">The email address to encrypt.</param>
        /// <returns>A Base64-encoded string containing the initialization vector (IV) and encrypted email.</returns>
        /// <remarks>
        /// The IV is prepended to the encrypted data, allowing for decryption without storing it separately.
        /// Uses AES encryption in CBC mode with PKCS7 padding.
        /// </remarks>
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

        /// <summary>
        /// Decrypts an encrypted email address.
        /// </summary>
        /// <param name="encryptedEmail">The Base64-encoded encrypted email from <see cref="EncryptionEmail"/>.</param>
        /// <returns>The decrypted email address in plain text.</returns>
        /// <remarks>
        /// Extracts the IV from the beginning of the encrypted data and uses it to decrypt the email.
        /// </remarks>
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
