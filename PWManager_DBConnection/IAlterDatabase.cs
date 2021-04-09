using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWManager_DBConnection
{
    interface IAlterDatabase
    {
        void SaveDatabaseTable(DataTable Table);
        void CreateDatabase(string strServerName, string strDatabaseName);
        void CreateDatabaseTable(string strServerName, string strDatabaseName,
                                 string strTableName, string strTableStructure);
        void AlterDatabaseTable(string strServerName, string strDatabaseName,
                                string strTableName, string strTableStructure);
        int InsertRecord(string strServerName, string strDatabaseName,
                         string strTableName, string strColumnNames,
                         string strColumnValues);
        int InsertParentRecord(string strConnectionString, string strTableName,
                               string strColumnNames, string strColumnValues);
        bool UpdateRecord(string strServerName, string strDatabaseName,
                          string strTableName, string strColumnNameAndValues,
                          string strCriteria);
        int DeleteRecord(string strConnectionString, string strTableName, string strPKName,
                         string strPKID);
    }
}
