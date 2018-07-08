using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DocsPublisher.Program.Core
{
    class DBCore
    {
        private static SqlConnection _sqlConn;

        //private static string SERVER;

        public static SqlConnection SQLConn;

        private object dbQueryStmt { get; set; }
        private object dbQueryResult { get; set; }

        SqlCommand dbCommand;

        public DBCore(string serverName)
        {
            if (SQLConn != null) SQLConn = null;

            SQLConn = ConnectDB(serverName);
        }

        public SqlConnection ConnectDB(string SERVER)
        {
            try
            {
                if (_sqlConn == null)
                {
                    _sqlConn = new SqlConnection();
                    _sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings[SERVER].ConnectionString;
                }
                if (_sqlConn.State == ConnectionState.Closed)
                {
                    _sqlConn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return _sqlConn;
        }

        public T DBResult<T>(SqlCommand dbCommand)
        {
            if (typeof(T) == typeof(DataSet))
                dbQueryResult = ExecuteDataSet(dbCommand);

            if (typeof(T) == typeof(DataTable))
                dbQueryResult = ExecuteDataTable(dbCommand);

            return (T)(object)dbQueryResult;

        }

        public void DBClose()
        {
            if (_sqlConn?.State == ConnectionState.Open)
                _sqlConn.Close();
        }

        public T InsertIntoDB<T>(string table, Dictionary<string, string> parameters)
        {
            dbCommand = new SqlCommand($"INSERT INTO {table}");
            dbCommand = BindToDB(dbCommand, parameters);
            return DBResult<T>(dbCommand);
        }

        public T SelectFromDB<T>(string table, string column = "*", Dictionary<string, string> parameters = null)
        {
            dbCommand = new SqlCommand($"SELECT {column} FROM {table} ORDER BY 1");
            dbCommand = BindToDB(dbCommand, parameters);
            return DBResult<T>(dbCommand);
        }

        public T ExecuteProcedure<T>(string procedureName, Dictionary<string, string> parameters = null)
        {
            dbCommand = new SqlCommand(procedureName, SQLConn);
            dbCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null) BindToDB(dbCommand, parameters);
            return DBResult<T>(dbCommand);
        }

        public void RunStoredProcedure(string dbProcStatement, Dictionary<string, string> parameters = null)
        {
            using (SqlCommand dbCommand = new SqlCommand(dbProcStatement, SQLConn))
            {
                if (parameters != null) BindToDB(dbCommand, parameters);
                dbCommand.ExecuteNonQuery();
            }
        }

        public SqlCommand BindToDB(SqlCommand dbCommand, Dictionary<string, string> parameters = null)
        {
            if (parameters?.Count > 0)
                foreach (var param in parameters)
                    dbCommand.Parameters.Add(param);
            return dbCommand;
        }

        protected static DataSet ExecuteDataSet(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql, SQLConn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        protected static DataSet ExecuteDataSet(SqlCommand cmd)
        {
            cmd.Connection = SQLConn;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        protected static DataTable ExecuteDataTable(string sql)
        {
            DataSet ds = ExecuteDataSet(sql);
            return ds.Tables[0];
        }

        protected static DataTable ExecuteDataTable(SqlCommand cmd)
        {
            DataSet ds = ExecuteDataSet(cmd);
            return ds.Tables[0];
        }

        protected object ExecuteScalar(SqlCommand cmd)
        {
            return cmd.ExecuteScalar();
        }
        protected object ExecuteScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, SQLConn);
            return cmd.ExecuteScalar();
        }
    }
}
