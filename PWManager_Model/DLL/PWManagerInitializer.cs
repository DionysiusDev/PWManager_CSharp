using PWManager_DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //calls the create database tables method
            CreateDatabaseTables();

            //generates test data for the database tables
            //SeedDatabaseTables();
        }

        /// <summary>
        /// This method will create the database tables required on SQL server
        /// </summary>
        private static void CreateDatabaseTables()
        {
            string connectionString = $"Data Source={_ServerName}; Initial Catalog={_DatabaseName}; Integrated Security=True";

            // Create Login table structure
            string LoginDataStructure = " AdminId int IDENTITY(1,1) PRIMARY KEY, UserName NVARCHAR(50) NOT NULL," +
                                        " Password NVARCHAR(MAX) NOT NULL ";

            //checks if the Login table doesn't already exist
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "Login"))
            {
                //creates the Login table with the LoginDataStructure
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "Login", LoginDataStructure);
            }

            // Create Password Info table structure
            string PasswordInfoDataStructure = " PwId int IDENTITY(1,1) PRIMARY KEY, " +
                "Website NVARCHAR(50) NOT NULL, " +
                "Email NVARCHAR(MAX) NOT NULL, " +
                "AdditionalInfo NVARCHAR(50), " +
                "Password NVARCHAR(50) NOT NULL";

            //checks if the Password Info table doesn't already exist
            if (!_sql.IsDatabaseTableExists(_ServerName, _DatabaseName, "PasswordInfo"))
            {
                //creates the Password Info table with the PasswordInfoDataStructure
                _sql.CreateDatabaseTable(_ServerName, _DatabaseName, "PasswordInfo", PasswordInfoDataStructure);
            }
        }


        /// <summary>
        /// Will populate the database tables and their related data.
        /// </summary>
        private static void SeedDatabaseTables()
        {
            SeedLoginDetails();
            //SeedCreateData();
        }

        /// <summary>
        /// Seeds login data.
        /// </summary>
        private static void SeedLoginDetails()
        {
            List<string> logins = new List<string>
            {
                "1, 'd', 'd'",

            };

            foreach (var login in logins)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "Login", "AdminId, UserName," +
                    " Password", login);

            }
        }

        /// <summary>
        /// Seeds create new password data.
        /// </summary>
        private static void SeedCreateData()
        {
            List<string> pws = new List<string>
            {
                "1, 'TestSite1', 'TestEmail1', 'No Additional Info 1', '1234Pw'",
                "2, 'TestSite2', 'TestEmail2', 'No Additional Info 2', '4321Pw'",

            };

            foreach (var pw in pws)
            {
                _sql.InsertRecord(_ServerName, _DatabaseName, "PasswordInfo", "PwId, Website, Email, AdditionalInfo, Password", pw);

            }
        }
    }
}