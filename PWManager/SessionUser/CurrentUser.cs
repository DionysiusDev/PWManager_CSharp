using SecurityAccessLayer;

namespace PWManager.SessionUser
{
    /// <summary>
    /// This class manages the currently signed in user,
    /// it sets the user's name,
    /// finds the user's secret key file for database access
    /// resets the user on log out or application exit
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// stores the current users user name
        /// </summary>
        public static string _UserName { get; set; }

        /// <summary>
        /// creates a file name for the specified user
        /// </summary>
        /// <param name="strUserName">users name</param>
        /// <returns>users file name</returns>
        public string CreateFileName(string strUserName)
        {
            // gets the hashcode of the user name for saving their file
            string userFileName = (strUserName.GetHashCode() * 7).ToString() + (strUserName.GetHashCode() * 13).ToString();
            return userFileName;
        }

        /// <summary>
        /// sets the current session users details
        /// </summary>
        /// <param name="strUserName">users name</param>
        public void SetUser(string strUserName)
        {
            _UserName = strUserName;                // sets the user name value
            FileHandling.SetFileName(_UserName);    // sets the file name for reading and saving key to

            // sets the user key for encrypting decrypting data
            SecurityAccessor.SetKey(FileHandling.ReadFromBinaryFile<byte[]>(FileHandling.GetFilePath()));
        }

        /// <summary>
        /// resets the user details for the next person who logs in
        /// </summary>
        public void ResetUser()
        {
            SecurityAccessor.SetKey(null);          // resets the user key
            _UserName = null;                       // resets the current user user name value
            FileHandling.ResetFilePath();           // resets the file path
        }
    }
}
