using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;

namespace PWManager_DBConnection
{
    public class SQL : IQueryDatabase, IAlterDatabase
    {
        #region Variable Declarations

        /// <summary>
        /// The connections string of the SQL database.
        /// </summary>
        public string ConnectionString { get; set; }

        #endregion

        #region Mutators

        /// <summary>
        /// This method will alter the specified database table on a specified
        /// server and database
        /// </summary>
        /// <param name="strServerName">Destination Server</param>
        /// <param name="strDatabaseName">Destination Database</param>
        /// <param name="strTableName">Table Name</param>
        /// <param name="strTableStructure">Table Structure</param>
        public void AlterDatabaseTable(string strServerName, string strDatabaseName, string strTableName, string strTableStructure)
        {
            try
            {
                ConnectionString = $"Data Source={strServerName}; " +
                                          $"Initial Catalog={strDatabaseName}; " +
                                          $"Integrated Security=True";

                string sqlQuery = $"ALTER TABLE {strTableName} ({strTableStructure})";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Alter Database Table] Error Altering Data Table " + e);
            }
        }

        /// <summary>
        /// This method will create a database on a specified server
        /// </summary>
        /// <param name="strServerName">Destination Server</param>
        /// <param name="strDatabaseName">Database Name</param>
        public void CreateDatabase(string strServerName, string strDatabaseName)
        {
            try
            {
                if (IsDatabaseExists(strServerName, strDatabaseName)) return;

                ConnectionString = $"Data Source={strServerName}; " +
                                          $"Integrated Security=True";
                string sqlQuery = $"CREATE DATABASE {strDatabaseName}";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Create Database] Error Creating Database " + e);
            }
        }

        /// <summary>
        /// This method will create a database table on a specified server
        /// and database.
        /// </summary>
        /// <param name="strServerName">Destination Server</param>
        /// <param name="strDatabaseName">Destination Database</param>
        /// <param name="strTableName">Table Name</param>
        /// <param name="strTableStructure">Table Structure</param>
        public void CreateDatabaseTable(string strServerName, string strDatabaseName, string strTableName, string strTableStructure)
        {
            try
            {
                ConnectionString = $"Data Source={strServerName}; " +
                                          $"Initial Catalog={strDatabaseName};" +
                                          $"Integrated Security=True";

                string sqlQuery = $"IF NOT EXISTS (SELECT name FROM sysobjects " +
                                  $"WHERE name = '{strTableName}') " +
                                  $"CREATE TABLE {strTableName} ({strTableStructure})";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Create Database Table] Error Creating Database Table " + e);
            }
        }

        /// <summary>
        /// This method will insert a record in the database.
        /// </summary>
        /// <param name="strConnectionString">Connection String</param>
        /// <param name="strTableName">Destination Table</param>
        /// <param name="strColumnNames">Column Names of the table</param>
        /// <param name="strColumnValues">Column Values</param>
        /// <returns>int NewId</returns>
        public int InsertParentRecord(string strConnectionString, string strTableName, string strColumnNames, string strColumnValues)
        {
            int Id = 0;

            try
            {
                string sqlQuery = $"INSERT INTO {strTableName} ({strColumnNames}) " +
                                  $"VALUES ({strColumnValues}) " +
                                  $"SELECT SCOPE_IDENTITY()";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        Id = (int)(decimal)command.ExecuteScalar();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Insert Parent Record] Error Inserting Record " + e);
            }

            return Id;
        }

        /// <summary>
        /// This method will insert a record in the database.
        /// </summary>
        /// <param name="strServerName">Destination Server</param>
        /// <param name="strDatabaseName">Destination Database</param>
        /// <param name="strTableName">Table Name</param>
        /// <param name="strColumnNames">Column Names of the table</param>
        /// <param name="strColumnValues">Column Values</param>
        /// <returns>int NewId</returns>
        public int InsertRecord(string strServerName, string strDatabaseName, string strTableName, string strColumnNames, string strColumnValues)
        {
            int Id = 0;

            ConnectionString =
                $"Data Source={strServerName}; Initial Catalog={strDatabaseName}; Integrated Security=True";

            string sqlQuery = $"SET IDENTITY_INSERT {strTableName} ON INSERT INTO {strTableName} ({strColumnNames}) VALUES ({strColumnValues}) SET IDENTITY_INSERT {strTableName} OFF";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        Id = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Insert Record] Error Inserting Record " + e);
            }

            return Id;
        }

        /// <summary>
        /// This method will update a database table.
        /// </summary>
        /// <param name="Table">Source Table</param>
        public void SaveDatabaseTable(DataTable Table)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {Table.TableName}", conn))
                    {
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Update(Table);
                        conn.Close();
                        Table.AcceptChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Save Database Table] Error Saving Data Table " + e);
                throw;

            }
        }

        /// <summary>
        /// This method will update a record in the database.
        /// </summary>
        /// <param name="strServerName">Destination Server</param>
        /// <param name="strDatabaseName">Destination Database</param>
        /// <param name="strTableName">Table Name</param>
        /// <param name="strColumnNameAndValues">Column Names and corresponding values</param>
        /// <param name="strCriteria">Update Criteria</param>
        /// <returns>bool IsOk</returns>
        public bool UpdateRecord(string strServerName, string strDatabaseName, string strTableName, string strColumnNamesAndValues, string strCriteria)
        {
            bool IsOK = false;

            ConnectionString =
                $"Data Source={strServerName}; Initial Catalog={strDatabaseName}; Integrated Security=True";

            string sqlQuery = $"UPDATE {strTableName} SET {strColumnNamesAndValues} WHERE {strCriteria}";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        IsOK = true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Update Record] Error Updating Record " + e);
                IsOK = false;
            }

            return IsOK;
        }

        /// <summary>
        /// This method will delete a record in the database
        /// </summary>
        /// <param name="strConnectionString">Connection String</param>
        /// <param name="strTableName">Table Name</param>
        /// <param name="strPKName">Primary Key Name</param>
        /// <param name="strPKID">Primary Key ID</param>
        /// <returns>int PKID</returns>
        public int DeleteRecord(string strConnectionString, string strTableName, string strPKName,
                         string strPKID)
        {
            int Id = 0;

            string sqlQuery = $"DELETE FROM {strTableName} WHERE {strPKName} = {strPKID}";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        Id = (int)(decimal)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Delete Record] Error Deleting Record " + e);
            }

            return Id;
        }

        #endregion

        #region Accessors

        /// <summary>
        /// This method will get an updateable table from the database.
        /// </summary>
        /// <param name="strTableName">Source Table</param>
        /// <returns>Datatable</returns>
        public DataTable GetDataTable(string strTableName)
        {
            DataTable Table = new DataTable
            {
                TableName = strTableName
            };


            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {strTableName}", conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Fill(Table);
                        conn.Close();
                        Table.PrimaryKey = new DataColumn[] { Table.Columns[0] };
                        Table.Columns[0].AutoIncrement = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Get Data Table] Error getting Data Table " + e);
            }

            return Table;
        }

        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="strTableName">Source Table</param>
        /// <param name="blnIsReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string strTableName, bool blnIsReadOnly)
        {
            if (blnIsReadOnly == false) return GetDataTable(strTableName);

            DataTable Table = new DataTable
            {
                TableName = strTableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand($"SELECT * FROM {strTableName}", conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Table.Load(reader);
                            conn.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Get Data Table] Error Getting Read Only Data Table " + e);
            }

            return Table;
        }

        /// <summary>
        /// This method will get an updateable table from the database.
        /// </summary>
        /// <param name="strSQLQuery">SQL query to retrieve records.</param>
        /// <param name="strTableName">Source Table</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string strSQLQuery, string strTableName)
        {
            DataTable Table = new DataTable
            {
                TableName = strTableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(strSQLQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Fill(Table);
                        conn.Close();
                        Table.PrimaryKey = new DataColumn[] { Table.Columns[0] };
                        Table.Columns[0].AutoIncrement = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Get Data Table] Error Getting Data Table " + e);
            }

            return Table;
        }

        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="strSQLQuery">SQL query to retrieve records.</param>
        /// <param name="strTableName">Source Table<</param>
        /// <param name="blnIsReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string strSQLQuery, string strTableName, bool blnIsReadOnly)
        {
            if (blnIsReadOnly == false) return GetDataTable(strSQLQuery, strTableName);

            DataTable Table = new DataTable
            {
                TableName = strTableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(strSQLQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Table.Load(reader);
                            conn.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Get Data Table] Error Getting Read Only Data Table " + e);
            }

            return Table;
        }

        #endregion

        #region Helper Methods

        public bool IsLoginVerified(string strUserName, string strPassword)
        {
            try
            {
                string sqlQuery = $"Select * from Login " +
                                    $"WHERE UserName='{strUserName}' " +
                                    $"AND Password='{strPassword}'";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();

                        //  start the transaction immediately after opening the connection
                        SqlTransaction loginTransaction = conn.BeginTransaction();

                        // link the transaction to the sql command
                        command.Transaction = loginTransaction;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataSet loginDataSet = new DataSet();

                        dataAdapter.Fill(loginDataSet);

                        int count = loginDataSet.Tables[0].Rows.Count;

                        // executes query in order to process the roll back
                        if (count == 1)
                        {
                            // commits the transaction
                            loginTransaction.Commit();

                            conn.Close();

                            return true;
                        }
                        else
                        {
                            try
                            {
                                // roll back the login transaction
                                loginTransaction.Rollback();
                            }
                            catch (Exception rollbackEx)
                            {
                                Logger.LogError("[PWManager_DBConnection] [SQL] [Is Login Verified] Error Rolling Back Transaction " + rollbackEx);
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Is Login Verified] Error connecting to Database " + e);
            }
            return false;
        }

        public bool IsEntryExists(string strWebsite, string strPassword)
        {
            string sqlQuery = $"SELECT * FROM PasswordInfo WHERE Website='{strWebsite}' AND Password='{strPassword}'";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        object objResult = command.ExecuteScalar();
                        conn.Close();
                        return (objResult != null);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Is Entry Exists] Error connecting to Database " + e);
            }
            return false;
        }
        
        public bool IsDatabaseExists(string strServerName, string strDatabaseName)
        {
            ConnectionString = $"Data source={strServerName}; " +
                                      $"Integrated Security = True";

            string sqlQuery = $"SELECT  database_id FROM sys.databases WHERE Name = " +
                              $"'{strDatabaseName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        object objResult = command.ExecuteScalar();
                        conn.Close();
                        return (objResult != null);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Is Database Exists] Error connecting to Database " + e);
            }

            return true;
        }

        /// <summary>
        /// This method will check if the specified database table exists in the specified database on a specified database server
        /// </summary>
        /// <param name="ServerName">Source Database Server</param>
        /// <param name="DatabaseName">Source Database Name</param>
        /// <param name="TableName">Table Name to check</param>
        /// <returns>bool</returns>
        public bool IsDatabaseTableExists(string strServerName, string strDatabaseName, string TableName)
        {
            bool IsExists = false;

            ConnectionString =
                $"Data Source={strServerName}; Initial Catalog={strDatabaseName}; Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand($"SELECT 1 FROM {TableName} WHERE 1=0", conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteScalar();
                        IsExists = true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[PWManager_DBConnection] [SQL] [Is Database Table Exists] Error connecting to Database " + e);
                IsExists = false;
            }

            return IsExists;
        }

        DataTable IQueryDatabase.GetDataTable(string strTableName)
        {
            throw new NotImplementedException();
        }

        DataTable IQueryDatabase.GetDataTable(string strTableName, bool blnIsReadOnly)
        {
            throw new NotImplementedException();
        }

        DataTable IQueryDatabase.GetDataTable(string strSQLQuery, string strTableName)
        {
            throw new NotImplementedException();
        }

        DataTable IQueryDatabase.GetDataTable(string strSQLQuery, string strTableName, bool blnIsReadOnly)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
