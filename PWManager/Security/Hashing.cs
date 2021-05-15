using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Logging;

namespace PWManager.Security
{
    public static class Hashing
    {
        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int _SaltSize = 16;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int _HashSize = 20;

        /// <summary>
        /// Hashes a password and generates salt.
        /// </summary>
        public static string Hash(string strPassword)
        {
            // generate salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_SaltSize]);

            // hash password
            var pbkdf2 = new Rfc2898DeriveBytes(strPassword, _SaltSize, 100000);
            byte[] hash = pbkdf2.GetBytes(_HashSize);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, _SaltSize);
            Array.Copy(hash, 0, hashBytes, _SaltSize, _HashSize);

            // Turns the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            Logger.LogDebug("[Hashing] [Hash] Generated hashed password " + savedPasswordHash);

            return savedPasswordHash;
        }

        /// <summary>
        /// Compares the user password to the hashed password and verifies the user
        /// </summary>
        /// <param name="strPassword">the user entered password</param>
        /// <param name="strHashedPw">the hashed password</param>
        /// <returns>true if the password it authenticated</returns>
        public static bool VerifyHash(string strPassword, string strHashedPw)
        {
            string savedPasswordHash = strHashedPw;

            // extract the bytes from the hashed password
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // gets the salt
            byte[] salt = new byte[_SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, _SaltSize);

            // Computes the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(strPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(_HashSize);

            // compares the hased and user entered passwords
            for (int index = 0; index < 20; index++)
                if (hashBytes[index + _SaltSize] == hash[index]) return true;
            return false;
        }
    }
}
