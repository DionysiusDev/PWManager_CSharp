using Security;
using Logging;
using System.Text;

namespace SecurityAccessLayer
{
    public static class SecurityAccessor
    {
        #region Encryption/Decryption

        // AESGCM Class
        public static byte[] NewKey()
        {
            return SetKey(AESGCM.NewKey());
        }

        public static string SimpleEncrypt(string strSecretMessage)
        {
            return AESGCM.SimpleEncrypt(strSecretMessage, GetKey(), null);
        }

        public static string SimpleDecrypt(string encryptedMessage)
        {
            return AESGCM.SimpleDecrypt(encryptedMessage, GetKey(), 0);
        }

        // Key Class
        public static byte[] SetKey(byte[] KeyToSet)
        {
            return Key.SetKey(KeyToSet);
        }

        public static byte[] GetKey()
        {
            return Key.GetKey();
        }

        #endregion

        #region Password Hashing

        // Password Hashing Class
        public static string GenerateHash(string strPassword)
        {
            return PasswordHashing.GenerateHash(strPassword);
        }

        public static string GetSalt()
        {
            return PasswordHashing._Salt;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            return PasswordHashing.VerifyPassword(enteredPassword, storedHash, storedSalt);
        }

        #endregion
    }
}
