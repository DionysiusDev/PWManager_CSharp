using PWManager_DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWManager_Model.DLL
{
    public class PWManagerContext
    {

        #region Variable Declarations

        public static string ConnectionString { get; set; }

        //add reference to sql class
        private static SQL _sql = new SQL();

        #endregion

        #region Accessors

        public static DataTable GetDataTable(string strTableName)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.GetDataTable(strTableName);
        }

        public static DataTable GetDataTable(string strSQLQuery, string strTableName)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.GetDataTable(strSQLQuery, strTableName);
        }
        #endregion

        #region Mutators

        public static string GetPassword(string strUserName)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.GetPassword(strUserName);
        }

        public static bool IsUserExists(string strUserName)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.IsUserExists(strUserName);
        }

        public static bool IsEntryExists(string strTableName, string strWebsite, string strPassword)
        {
            _sql.ConnectionString = ConnectionString;
            return _sql.IsEntryExists(strTableName, strWebsite, strPassword);
        }

        // using non-Hungarian notation for variables
        public static void SaveDatabaseTable(DataTable Table)
        {
            _sql.ConnectionString = ConnectionString;
            _sql.SaveDatabaseTable(Table);
        }

        // using non-Hungarian notation for variables
        public static int InsertParentTable(string TableName, string ColumnNames,
                                            string ColumnValues)
        {
            return _sql.InsertParentRecord(ConnectionString, TableName, ColumnNames, ColumnValues);
        }

        public static int DeleteRecord(string strTableName, string strPKName,
                                       string strPKID)
        {
            return _sql.DeleteRecord(ConnectionString, strTableName, strPKName, strPKID);
        }
        #endregion
    }
}
