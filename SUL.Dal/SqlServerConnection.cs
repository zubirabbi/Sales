using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SUL.Dal
{
    public class SqlServerConnection
    {
        private readonly string _connectionString;

        private SqlConnection _sqlCon;


        protected SqlServerConnection()
        {
            _connectionString = SUL.Dal.Properties.Settings.Default.SQLConnectionStringLive;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean Connect()
        {
            _sqlCon = new SqlConnection();

            try
            {
                _sqlCon.ConnectionString = _connectionString;
                if (_sqlCon.State.ToString() != "Open")
                {
                    _sqlCon.Open();
                }

                return true;
                // You can get the server version 
                // MySqlConnect.ServerVersion
            }
            catch (Exception Ex)
            {
                // Try to close the connection
                if (_sqlCon != null)
                    _sqlCon.Dispose();

                throw new Exception(Ex.Message + "; connectionstring: " + _connectionString);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean Disconnect()
        {
            if (_sqlCon.State.ToString() == "Open")
            {
                try
                {
                    _sqlCon.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="KeyField"></param>
        /// <param name="ConditionString"></param>
        /// <returns></returns>
        protected int CheckExistence(string TableName, string KeyField, string ConditionString)
        {
            SqlCommand cmdSql = new SqlCommand();

            int functionReturnValue = 0;
            string sqlString = null;

            sqlString = "select count(" + KeyField + ") as cnt from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;
                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = int.Parse(val.ToString());

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                Disconnect();


                cmdSql.Dispose();

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="KeyField"></param>
        /// <param name="ConditionString"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected int CheckExistence(string TableName, string KeyField, string ConditionString, Hashtable lstData)
        {
            SqlCommand cmdSql = new SqlCommand();


            int functionReturnValue = 0;
            string sqlString = null;



            sqlString = "select count(" + KeyField + ") as cnt from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;


                if (lstData != null)
                {
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();

                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                    }
                }

                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = int.Parse(val.ToString());


                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                Disconnect();


                cmdSql.Dispose();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="DefaultID"></param>
        /// <param name="ConditionString"></param>
        /// <returns></returns>
        protected int GetMaximumID(string TableName, string FieldName, int DefaultID, string ConditionString)
        {
            int functionReturnValue = 0;
            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();


            sqlString = "select max(" + FieldName + ") as cnt from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }
            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;
                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = int.Parse(val.ToString());

                return functionReturnValue;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {


                Disconnect();

                cmdSql.Dispose();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="DefaultID"></param>
        /// <param name="ConditionString"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>

        protected int GetMaximumIDbyCondition(string TableName, string FieldName, int DefaultID, string ConditionString, Hashtable lstData)
        {
            int functionReturnValue = 0;
            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();


            sqlString = "select max(" + FieldName + ") as cnt from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }
            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;

                if (lstData != null)
                {
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();

                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                    }
                }

                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = int.Parse(val.ToString());

                return functionReturnValue;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {


                Disconnect();

                cmdSql.Dispose();

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Fields"></param>
        /// <param name="ConditionString"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string TableName, string Fields, string ConditionString)
        {

            string sqlString = null;
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            sqlString = "select " + Fields + " from " + TableName;

            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }

            DataTable dt = new DataTable();

            try
            {
                Connect();

                sqlAdapter = new SqlDataAdapter(sqlString, _sqlCon);
                sqlAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                sqlAdapter.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="querySting"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string querySting, Hashtable lstData)
        {
            DataTable dt = new DataTable();
            SqlCommand cmdSql = new SqlCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            //string sqlString = null;

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = querySting;

                if (lstData != null)
                {
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();

                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                    }
                }

                sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = cmdSql;
                sqlAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                sqlAdapter.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Fields"></param>
        /// <param name="ConditionString"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string TableName, string Fields, string ConditionString, Hashtable lstData)
        {

            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            sqlString = "select " + Fields + " from " + TableName;

            if (!string.IsNullOrEmpty(ConditionString))
                sqlString = sqlString + " " + ConditionString;

            DataTable dt = new DataTable();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;

                if (lstData != null)
                {
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();

                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                    }
                }

                sqlAdapter = new SqlDataAdapter(sqlString, _sqlCon);
                sqlAdapter.SelectCommand = cmdSql;
                sqlAdapter.Fill(dt);

                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                Disconnect();

                cmdSql.Dispose();

                sqlAdapter.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected DataTable GetDataTableFromSP(string SPName, Hashtable lstData)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            try
            {
                Connect();

                sqlAdapter = new SqlDataAdapter(SPName, _sqlCon);
                sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                if (lstData != null)
                {
                    SqlParameter sprmparam = new SqlParameter();
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();
                        sprmparam = new SqlParameter(dict.Key.ToString(), dict.Value.ToString());

                        sqlAdapter.SelectCommand.Parameters.Add(sprmparam);
                    }
                }

                sqlAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                sqlAdapter.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="ConditionString"></param>
        /// <returns></returns>
        protected string ExecuteScaler(string TableName, string FieldName, string ConditionString)
        {
            string functionReturnValue = "";
            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();

            sqlString = "select " + FieldName + " as val from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }
            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;

                object val = cmdSql.ExecuteScalar();
                if (val != null)
                    functionReturnValue = val.ToString();
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                Disconnect();

                cmdSql.Dispose();

            }

            return functionReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="ConditionString"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected string ExecuteScaler(string TableName, string FieldName, string ConditionString, Hashtable lstData)
        {
            string functionReturnValue = "";
            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();


            sqlString = "select " + FieldName + " as val from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }
            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;

                if (lstData != null)
                {
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();

                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                    }
                }

                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = val.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                Disconnect();

                cmdSql.Dispose();

            }

            return functionReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Fields"></param>
        /// <param name="ConditionString"></param>
        /// <returns></returns>
        protected DataSet GetDataSet(string TableName, string Fields, string ConditionString)
        {

            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();

            sqlString = "select " + Fields + " from " + TableName;

            if (!string.IsNullOrEmpty(ConditionString))
            {

                sqlString = sqlString + " " + ConditionString;

            }

            DataSet ds = new DataSet();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;
                sqlAdapter = new SqlDataAdapter(sqlString, _sqlCon);
                sqlAdapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                cmdSql.Dispose();

                sqlAdapter.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="QueryString"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string QueryString)
        {
            int functionReturnValue = 0;

            SqlCommand cmdSql = new SqlCommand();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = QueryString;

                functionReturnValue = cmdSql.ExecuteNonQuery();

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                cmdSql.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="filedata"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string QueryString, Byte[] filedata)
        {
            int functionReturnValue = 0;

            SqlCommand cmdSql = new SqlCommand();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = QueryString;

                cmdSql.Parameters.AddWithValue("@File", filedata);

                functionReturnValue = cmdSql.ExecuteNonQuery();

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                cmdSql.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(string QueryString, Hashtable lstData)
        {
            int functionReturnValue = 0;

            SqlCommand cmdSql = new SqlCommand();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = QueryString;

                foreach (DictionaryEntry dict in lstData)
                {
                    Type type = dict.Value.GetType();

                    if (type.Name == "Byte[]")
                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value);
                    else
                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                }

                functionReturnValue = cmdSql.ExecuteNonQuery();

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                cmdSql.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected DataTable ExecuteSelectQuery(string sqlString, string paramName, string value, DbType type)
        {
            SqlCommand cmdSql = new SqlCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;

                var param = cmdSql.Parameters.AddWithValue(paramName, value);
                param.DbType = type;

                sqlAdapter = new SqlDataAdapter(sqlString, _sqlCon);
                sqlAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();
                cmdSql.Dispose();
                sqlAdapter.Dispose();
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected int ExecuteStoreProcedure(string procedureName, Hashtable lstData)
        {
            int functionReturnValue = 0;

            SqlCommand cmdSql = new SqlCommand();

            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = procedureName;
                cmdSql.CommandType = CommandType.StoredProcedure;

                foreach (DictionaryEntry dict in lstData)
                {
                    Type type = dict.Value.GetType();

                    if (type.Name == "Byte[]")
                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value);
                    else
                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                }

                functionReturnValue = cmdSql.ExecuteNonQuery();

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                cmdSql.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="DefaultID"></param>
        /// <param name="ConditionString"></param>
        /// <returns></returns>
        protected int GetMinimumID(string TableName, string FieldName, int DefaultID, string ConditionString)
        {
            int functionReturnValue = 0;
            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();


            sqlString = "select min(" + FieldName + ") as cnt from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }
            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;
                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = int.Parse(val.ToString());

                return functionReturnValue;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Disconnect();

                cmdSql.Dispose();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldName"></param>
        /// <param name="DefaultID"></param>
        /// <param name="ConditionString"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        protected int GetMinimumID(string TableName, string FieldName, int DefaultID, string ConditionString, Hashtable lstData)
        {
            int functionReturnValue = 0;
            string sqlString = null;

            SqlCommand cmdSql = new SqlCommand();


            sqlString = "select min(" + FieldName + ") as cnt from " + TableName;
            if (!string.IsNullOrEmpty(ConditionString))
            {
                sqlString = sqlString + " " + ConditionString;
            }
            try
            {
                Connect();
                cmdSql.Connection = _sqlCon;
                cmdSql.CommandText = sqlString;

                if (lstData != null)
                {
                    foreach (DictionaryEntry dict in lstData)
                    {
                        Type type = dict.Value.GetType();

                        cmdSql.Parameters.AddWithValue(dict.Key.ToString(), dict.Value.ToString());
                    }
                }

                object val = cmdSql.ExecuteScalar();

                if (val != null)
                    functionReturnValue = int.Parse(val.ToString());

                return functionReturnValue;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {


                Disconnect();

                cmdSql.Dispose();

            }

        }

    }
}
