using PWManager.Security;
using System.Text;

namespace PWManager.SessionUser
{
    public static class CurrentUser
    {
        public static string _UserName { get; set; }

        public static void SetUser(string strUserName)
        {
            // sets the user name value
            _UserName = strUserName;
            Logging.Logger.LogDebug($"[Current User] [Set User] Setting Current User : {_UserName}");

            // sets the directory for the user key file
            FileHandling._Directory = "AppData";

            // sets the file name for reading and saving key to
            KeyManager.SetFileName(_UserName);
            Logging.Logger.LogDebug($"[Current User] [Set User] Setting Key Filepath : {FileHandling.GetFilePath()}");

            // sets the user key for encrypting decrypting data
            Key.DbKey = FileHandling.ReadFromBinaryFile<byte[]>(FileHandling.GetFilePath());
            Logging.Logger.LogDebug($"[Current User] [Set User] Setting Key : {Encoding.UTF8.GetString(Key.DbKey)}");
        }

        public static void ResetUser()
        {
            Key.DbKey = null;   // resets the user key
            Logging.Logger.LogDebug($"[Current User] [Reset User] Resetting Key : {Key.DbKey == null}");

            CurrentUser._UserName = null;   // resets the current user user name value
            Logging.Logger.LogDebug($"[Current User] [Reset User] Resetting Current User : {string.IsNullOrEmpty(_UserName)}");

            KeyManager._FileName = _UserName;  // resets the file name for reading and saving key to
            Logging.Logger.LogDebug($"[Current User] [Reset User] Resetting Key File Name : {string.IsNullOrEmpty(KeyManager._FileName)}");

            // resets the file path
            Logging.Logger.LogDebug($"[Current User] [Reset User] Resetting File Path : {string.IsNullOrEmpty(FileHandling.ResetFilePath())}");
        }
    }
}
