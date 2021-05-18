using System;
using System.Security.Cryptography;

namespace Security
{
    public static class PasswordHashing
    {
        public static string _Hash { get; set; }
        public static string _Salt { get; set; }

        /// <summary>
        /// generates a string of salt for storing with user password
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateSalt()
        {
            var saltBytes = new byte[16];   // creates a new arry of bytes for the salt

            // instantiates a new RNGCryptoServiceProvider
            var provider = new RNGCryptoServiceProvider();

            // fills salt bytes with a cryptographically strong random sequence of nonzero values
            provider.GetNonZeroBytes(saltBytes);

            // converts salt bytes to string for storing
            var salt = Convert.ToBase64String(saltBytes);

            _Salt = salt;   // sets the _Salt global variable

            return saltBytes;
        }

        /// <summary>
        /// hashes a users password
        /// </summary>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static string GenerateHash(string strPassword)
        {
            // produces a derived key from password, salt value and an iteration count.
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(strPassword, GenerateSalt(), 10000);

            // converts rfc2898DeriveBytes to string for storing
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            _Hash = hashPassword;   // sets the _Hash global variable

            return hashPassword;
        }

        /// <summary>
        /// verifies the user entered password with the stored password
        /// </summary>
        /// <param name="enteredPassword">user entered password</param>
        /// <param name="storedHash">stored password hash</param>
        /// <param name="storedSalt">stored salt</param>
        /// <returns></returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // converts the stored salt to bytes
            var saltBytes = Convert.FromBase64String(storedSalt);

            // produces a derived key from user entered password, stored salt and an iteration count.
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);

            // converts rfc2898DeriveBytes to string and compares with the stored hash
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}
