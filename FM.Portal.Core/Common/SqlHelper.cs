using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Core.Common
{
    public class SQLHelper
    {
        public static SqlConnection OpenConnection(SqlConnection sqlconnection)
        {
            try
            {
                if (sqlconnection.State == ConnectionState.Closed)
                    sqlconnection.Open();
                return sqlconnection;
            }
            catch
            {
                return null;
            }
        }
        public static void BatchExcute(SqlCommand[] commands)
        {
            if (commands?.Length < 1)
                return;

            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionString()))
            {
                sqlconnection.Open();
                using (var tran = sqlconnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var command in commands)
                        {
                            command.Connection = sqlconnection;
                            command.Transaction = tran;
                            command.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        throw e;
                    }
                    finally
                    {
                        //clearContextInfo(connection, contextInfo);
                        sqlconnection.Close();
                    }
                }

            }
        }
        public static SqlCommand CreateCommand(string queryName, System.Data.CommandType commandtype, SqlParameter[] parameters)
        {
            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand com = new SqlCommand(queryName, sqlconnection);
                com.CommandType = commandtype;
                if (parameters != null)
                    foreach (SqlParameter parm in parameters)
                    {
                        object parm2 = parm.Value;
                        if (parm2 == null)
                            parm.Value = DBNull.Value;
                    }

                if (parameters != null)
                    com.Parameters.AddRange(parameters);
                return com;
            }
                
        }
        public SqlParameter BasaSqlParameter(string parameterName, object value)
        {
            SqlParameter sp;
            if (value.ToString() != string.Empty)
                sp = new SqlParameter(parameterName, value);
            else
                sp = new SqlParameter(parameterName, DBNull.Value);
            return sp;
        }
        public static string ExecuteNonQuery(SqlConnection sqlconnection, CommandType commandtype, SqlParameter[] parameters, string queryName)
        {
            SqlCommand com = new SqlCommand(queryName, sqlconnection);
            com.CommandType = commandtype;
            sqlconnection = OpenConnection(sqlconnection);
            if (parameters != null)
                foreach (SqlParameter parm in parameters)
                {
                    object parm2 = parm.Value;
                    if (parm2 == null)
                        parm.Value = DBNull.Value;

                    //if ((SQLHelper.CheckIntNull(parm2) == 0) && ((parm.DbType == DbType.Int16) ||
                    //    (parm.DbType == DbType.Int32) || (parm.DbType == DbType.Int64) ||
                    //    (parm.DbType == DbType.Byte) || (parm.DbType == DbType.UInt16) ||
                    //    (parm.DbType == DbType.UInt32) || (parm.DbType == DbType.UInt64) ||(parm.DbType == DbType.Guid)
                    //    ))
                    //    parm.Value = DBNull.Value;
                }

            if (parameters != null)
                com.Parameters.AddRange(parameters);
            try
            {
                return com.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static int ExecuteNonQuery(SqlConnection sqlconnection, CommandType commandtype, string queryName, SqlParameter[] parameters)
        {
            SqlCommand com = new SqlCommand(queryName, sqlconnection);
            com.CommandType = commandtype;
            sqlconnection = OpenConnection(sqlconnection);
            if (parameters != null)
                foreach (SqlParameter parm in parameters)
                {
                    object parm2 = parm.Value;
                    if (parm2 == null)
                        parm.Value = DBNull.Value;
                }

            if (parameters != null)
                com.Parameters.AddRange(parameters);
            try
            {
                return com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static SqlDataReader ExecuteReader(SqlConnection sqlconnection, CommandType commandtype, string queryName) { return ExecuteReader(sqlconnection, commandtype, queryName, null); }
        public static SqlDataReader ExecuteReader(SqlConnection sqlconnection, CommandType commandtype, string queryName, SqlParameter[] parameters)
        {
            SqlCommand com = new SqlCommand(queryName, sqlconnection);
            com.CommandType = commandtype;
            sqlconnection = OpenConnection(sqlconnection);
            if (parameters != null)
                com.Parameters.AddRange(parameters);
            try
            {
                return com.ExecuteReader();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static object ExecuteScalar(SqlConnection sqlconnection, CommandType commandtype, string queryName, SqlParameter[] parameters)
        {
            SqlCommand com = new SqlCommand(queryName, sqlconnection);
            com.CommandType = commandtype;
            sqlconnection = OpenConnection(sqlconnection);
            if (parameters != null)
                foreach (SqlParameter parm in parameters)
                {
                    object parm2 = parm.Value;
                    if (parm2 == null)
                        parm.Value = DBNull.Value;

                    if (SQLHelper.CheckDateTimeNull(parm2) == DateTime.MinValue && (parm.DbType == DbType.DateTime))
                    {
                        parm.Value = DBNull.Value;
                        parm.DbType = DbType.DateTime;
                        parm.SqlDbType = SqlDbType.SmallDateTime;
                    }

                    if ((SQLHelper.CheckLongNull(parm2) == 0) && ((parm.DbType == DbType.Int16) ||
                        (parm.DbType == DbType.Int32) || (parm.DbType == DbType.Int64) ||
                        (parm.DbType == DbType.Byte) || (parm.DbType == DbType.UInt16) ||
                        (parm.DbType == DbType.UInt32) || (parm.DbType == DbType.UInt64)
                        ))
                        parm.Value = DBNull.Value;
                }

            if (parameters != null)
                com.Parameters.AddRange(parameters);
            try
            {
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static DataTable GetDataTable(CommandType commandtype, string queryName, SqlParameter[] parameters)
        {
            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand com = new SqlCommand(queryName, sqlconnection);
                com.CommandType = commandtype;
                if (sqlconnection.State == ConnectionState.Closed)
                    sqlconnection.Open();
                if (parameters != null)
                    foreach (SqlParameter parm in parameters)
                    {
                        object parm2 = parm.Value;
                        if (parm2 == null)
                            parm.Value = DBNull.Value;

                        //if (SQLHelper.CheckDateTimeNull(parm2) == DateTime.MinValue && (parm.DbType == DbType.DateTime))
                        //{
                        //    parm.Value = DBNull.Value;
                        //    parm.DbType = DbType.DateTime;
                        //    parm.SqlDbType = SqlDbType.SmallDateTime;
                        //}


                    }

                if (parameters != null)
                    com.Parameters.AddRange(parameters);
                DataTable dt = new DataTable();
                try
                {
                    dt.Load(com.ExecuteReader());
                    return dt;
                }
                catch(Exception e)
                {
                    return null;
                }

            }
        }
        public static DataTable GetDataTableByDataAdapter(CommandType commandtype, string queryName, SqlParameter[] parameters)
        {
            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(queryName, sqlconnection);
                da.SelectCommand.CommandType = commandtype;

                if (parameters != null)
                    da.SelectCommand.Parameters.AddRange(parameters);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    return dt;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
        }
        public static string CheckStringNull(object obj)
        {
            try
            {
                return obj.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static DateTime CheckDateTimeNull(object obj)
        {
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static int CheckIntNull(object obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static byte CheckByteNull(object obj)
        {
            try
            {
                return byte.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static bool CheckBoolNull(object obj)
        {
            try
            {
                return bool.Parse(obj.ToString());
            }
            catch
            {
                return false;
            }
        }
        public static long CheckLongNull(object obj)
        {
            try
            {
                return long.Parse(obj.ToString());
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        public static decimal CheckDecimalNull(object obj)
        {
            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static Guid CheckGuidNull(object obj)
        {
            try
            {
                Guid value;
                Guid.TryParse(obj.ToString(), out value);
                return value;
            }
            catch
            {
                return Guid.Empty;
            }
        }
        public static double CheckDoubleNull(object obj)
        {
            try
            {
                return double.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static float CheckFloatNull(object obj)
        {
            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }

    }
}
