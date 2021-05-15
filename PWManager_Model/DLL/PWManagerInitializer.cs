using PWManager_DBConnection;
using System.Collections.Generic;
using System.IO;
using Logging;

namespace PWManager_Model.DLL
{
    // Make this a static class so we can call it without creating an instance of it
    public static class PWManagerInitializer
    {
        //TODO: Replace the ServerName with the name of your server and replace the database instance
        private static string _ServerName = @"DESKTOP-51EGPI0\SSSDB2016MSSQL", _DatabaseName = "PWManager_DB";

        //add reference to pw manager db connection - add using PWManager_DBConnection to access sql class
        static SQL _sql = new SQL();

        /// <summary>
        /// This method will create the database on the specified SQL Server
        /// </summary>
        public static void CreateDatabase()
        {
            // Create the Database at the provided server name
            _sql.CreateDatabase($"{_ServerName}", $"{_DatabaseName}");

            // creates the login table
            CreateLoginTable();
        }

        /// <summary>
        /// This method will create the login database table required on SQL server
        /// </summary>
        public static void CreateLoginTable()
        {
            string connectionString = $"Data Source={_ServerName}; Initial Catalog={_DatabaseName}; Integrated Security=True";

            string _TableName = "Login";

            // Create Login table structure
            string LoginDataStructure = " UserId int IDENTITY(1,1) PRIMARY KEY, UserName NVARCHAR(50) NOT NULL," +
                                        " Password NVARCHAR(MAX) NOT NULL";

            //checks if the Login table doesn't already exist
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, _TableName))
            {
                //creates the Login table with the LoginDataStructure
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, _TableName, LoginDataStructure);
            }
        }

        /// <summary>
        /// Adds user login details to the database
        /// </summary>
        public static void AddUserLogin(string strUserName, string strHashedPw)
        {
            string _TableName = "Login";

            // gets the count from the user id column
            int count = _sql.GetCount(_ServerName, _DatabaseName, _TableName, "UserId");
            // increments the count for the next id
            int nextID = count + 1;

            // inserts the user into the database
            _sql.InsertRecord(_ServerName, _DatabaseName, _TableName, "UserId, UserName, Password", $"{nextID}, '{strUserName}', '{strHashedPw}'", nextID);
        }

        /// <summary>
        /// This method will create the user password info database tables required on SQL server
        /// </summary>
        public static void CreateUserPasswordTables(string strUserName)
        {
            string _TableName = $"{strUserName}Passwords";

            // Create Password Info table structure
            string PasswordInfoDataStructure = " PwId int IDENTITY(1,1) PRIMARY KEY, " +
                "Website NVARCHAR(100) NOT NULL, " +
                "Email NVARCHAR(100) NOT NULL, " +
                "AdditionalInfo NVARCHAR(100), " +
                "Password NVARCHAR(100) NOT NULL";

            //checks if the Password Info table doesn't already exist
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, _TableName))
            {
                //creates the Password Info table with the PasswordInfoDataStructure
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, _TableName, PasswordInfoDataStructure);
            }
        }
    }
}