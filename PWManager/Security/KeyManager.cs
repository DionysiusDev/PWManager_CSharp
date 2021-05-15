using System.IO;

namespace PWManager.Security
{
    public static class KeyManager
    {
        public static string _FileName { get; set; }

        /// <summary>
        /// this method creates the key for encrypting and decrypting the database
        /// </summary>
        public static void CreateApplicationKey(string strUserName)
        {
            // gets the hashcode of the user name for saving their file
            string userFileName = (strUserName.GetHashCode() * 7).ToString() + (strUserName.GetHashCode() * 13).ToString();

            FileHandling._FileName = userFileName;

            // creates the directory and file path
            Directory.CreateDirectory(FileHandling._Directory);
            string _FilePath = Path.Combine(FileHandling._Directory, FileHandling._FileName);

            // checks if the file exists
            if (!System.IO.File.Exists(_FilePath))
            {
                Key.DbKey = AESGCM.NewKey(); // creates a new key object
                byte[] _Key = Key.DbKey;

                FileHandling.WriteToBinaryFile(_FilePath, _Key, false);  // writes the key to file
            }
        }


        /// <summary>
        /// sets the file name for reading the users key
        /// </summary>
        /// <param name="strUserName"></param>
        public static void SetFileName(string strUserName)
        {
            // gets the hashcode of the user name for saving their file
            _FileName = (strUserName.GetHashCode() * 7).ToString() + (strUserName.GetHashCode() * 13).ToString();

            FileHandling._FileName = _FileName;
        }
    }
}
