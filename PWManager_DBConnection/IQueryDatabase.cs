using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWManager_DBConnection
{
    interface IQueryDatabase
    {
        DataTable GetDataTable(string strTableName);
        DataTable GetDataTable(string strTableName, bool blnIsReadOnly);
        DataTable GetDataTable(string strSQLQuery, string strTableName);
        DataTable GetDataTable(string strSQLQuery, string strTableName,
                               bool blnIsReadOnly);
    }
}
