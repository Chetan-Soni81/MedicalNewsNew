using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MedicalNews.DAL
{
    public class DataAccessLayer
    {
        #region Global Variables
        private SqlConnection? MyConn = null;
        private string? StringConnection = null;
        public string? StringConnection1 = null;
        #endregion
        //"server=ANAND;database=SMS;uid=sa;password=sa";
        //ConfigurationSettings.AppSettings["StrConn"].ToString();
        /// <summary>
        /// Method to open connection of database.

        private void OpenConnection()
        {
            if (MyConn == null)
            {
                StringConnection = "Server=MSI\\SQLEXPRESS; Database=MedicalNews; Trusted_connection=True;";
                //StringConnection = "server=SG2NWPLS19SQL-v07.mssql.shr.prod.sin2.secureserver.net; database=medicalnews; uid =medical; pwd=News#$%123; pooling=true; min pool size=0; max pool size=100";
                MyConn = new SqlConnection(StringConnection);
            }
            if (MyConn.State == ConnectionState.Closed || MyConn.State == ConnectionState.Broken)
            {
                MyConn.Open();
            }
        }
        private string ConncetionString()
        {

            StringConnection = "Server=MSI\\SQLEXPRESS; Database=MedicalNews; Trusted_connection=True";
            //StringConnection = "server=SG2NWPLS19SQL-v07.mssql.shr.prod.sin2.secureserver.net; database=medicalnews; uid =medical; pwd=News#$%123; pooling=true; min pool size=0; max pool size=100";

            return StringConnection;


        }
        private SqlCommand CreateCmd(string procName, params object[] ps)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = MyConn;
            SqlParameter[]? sqlpa = null;

            if (ps != null)
            {
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters.RemoveAt(0);
                sqlpa = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(sqlpa, 0);
                for (int i = 0; i < sqlpa.Length; ++i)
                {
                    sqlpa[i].Value = ps[i];
                }
            }
            return cmd;

        }
        private SqlCommand CreateCmd(string procName, SqlParameter[] param)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = MyConn;


            if (param != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(param);
            }
            return cmd;

        }


        #region Methods
        public SqlCommand CreateTransCmd(string procName, SqlTransaction trans, params object[] ps)
        {
            SqlCommand cmd = new SqlCommand(procName, MyConn, trans);
            try
            {
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = MyConn;
                SqlParameter[]? sqlpa = null;
                if (ps != null)
                {
                    SqlCommandBuilder.DeriveParameters(cmd);
                    cmd.Parameters.RemoveAt(0);
                    sqlpa = new SqlParameter[cmd.Parameters.Count];
                    cmd.Parameters.CopyTo(sqlpa, 0);
                    for (int i = 0; i < sqlpa.Length; ++i)
                    {
                        sqlpa[i].Value = ps[i];
                    }
                }
                return cmd;
            }
            catch (Exception)
            {
                trans.Rollback();
                MyConn.Close();
                return cmd;
            }
        }

        public SqlCommand CreateTransCmd(string procName, SqlTransaction trans, SqlParameter[] param)
        {
            SqlCommand cmd = new SqlCommand(procName, MyConn, trans);
            try
            {
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = MyConn;

                if (param != null)
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddRange(param);
                }
                return cmd;
            }
            catch (Exception)
            {
                trans.Rollback();
                MyConn.Close();
                return cmd;
            }
        }
        #endregion
        public DataTable SelectRecordByDatatable(string procName)
        {
            using (SqlCommand cmd = CreateCmd(procName, null))
            {
                using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    return dt;
                }
            }
        }
        public DataTable SelectRecordByDatatable(string procName, params object[] ps)
        {
            using (SqlCommand cmd = CreateCmd(procName, ps))
            {
                using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    return dt;
                }
            }
        }
        public DataSet SelectRecordByDataSet(string procName, params object[] ps)
        {

            using (SqlCommand cmd = CreateCmd(procName, ps))
            {
                using (SqlDataAdapter sqlad = new SqlDataAdapter())
                {
                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            sqlad.SelectCommand = cmd;
                            sqlad.Fill(ds);
                        }
                        catch
                        {
                            return new DataSet();
                        }
                        return ds;
                    }
                }
            }
        }

        public DataSet SelectRecordByDataSet(string procName, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = CreateCmd(procName, parameters))
            {
                using (SqlDataAdapter sqlad = new SqlDataAdapter())
                {
                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            sqlad.SelectCommand = cmd;
                            sqlad.Fill(ds);
                        }
                        catch(Exception ex)
                        {
                            return new DataSet();
                        }
                        return ds;
                    }
                }
            }
        }
        public DataSet SelectRecordByDataSet(string procName)
        {
            using (SqlCommand cmd = CreateCmd(procName, null))
            {
                using (SqlDataAdapter sqlad = new SqlDataAdapter())
                {
                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            sqlad.SelectCommand = cmd;
                            sqlad.Fill(ds);
                        }
                        catch
                        {
                            return new DataSet();
                        }
                        return ds;
                    }
                }
            }
        }
        public int ExecuteNonQuery(string procName, params object[] ps)
        {
            int i = 0;
            using (SqlCommand cmd = CreateCmd(procName, ps))
            {
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return i;
            }
        }
        public int ExecuteNonQueryWithOutPutParameter(string procName, params object[] ps)
        {
            int i = 0;
            using (SqlCommand cmd = CreateCmd(procName, ps))
            {
                try
                {
                    i = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return i;
            }
        }
        public SqlDataReader SelectRecordBydataReader(string procName)
        {
            using (SqlCommand cmd = CreateCmd(procName, null))
            {
                SqlDataReader? sdr = null;
                return sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        public SqlDataReader SelectRecordBydataReader(string procName, params object[] ps)
        {
            using (SqlCommand cmd = CreateCmd(procName, ps))
            {
                SqlDataReader? sdr = null;
                return sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public SqlDataReader SelectRecordBydataReader(string CommandName, SqlParameter[] param)
        {


            using (SqlCommand cmd = CreateCmd(CommandName, param))
            {
                SqlDataReader? sdr = null;
                return sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }


        }
        public int ExecuteNonQuery(string CommandName, SqlParameter[] pars)
        {
            int result = 0;
            SqlTransaction trans;
            ConncetionString();
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }
                trans = con.BeginTransaction();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        result = cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        trans.Rollback();
                    }
                    finally
                    {
                        trans.Dispose();
                        con.Close();
                    }
                }
            }

            return result;
        }
        //public void Dispose()
        //{
        //	throw new NotImplementedException();
        //}

        public int ExecuteNonQueryParamIntTemp(string CommandName, SqlParameter[] pars)
        {
            int result = 0;

            SqlTransaction trans;
            ConncetionString();
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }
                trans = con.BeginTransaction();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = CommandName;
                    //pars[parano].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddRange(pars);
                    cmd.Parameters.Add("@Result", SqlDbType.Char, 500);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;



                    try
                    {
                        result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            result = Convert.ToInt32(cmd.Parameters["@Result"].Value.ToString());
                        }
                        trans.Commit();
                    }
                    catch (Exception EX)
                    {
                        trans.Rollback();
                    }
                    finally
                    {
                        trans.Dispose();
                        con.Close();
                    }
                }
            }

            return result;
        }


        public string ExecuteNonQueryParamStringTemp(string CommandName, SqlParameter[] pars)
        {
            int result = 0;
            string ReternResult = "0";
            SqlTransaction trans;
            ConncetionString();
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }
                trans = con.BeginTransaction();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = CommandName;
                    //pars[parano].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddRange(pars);
                    cmd.Parameters.Add("@Result", SqlDbType.Char, 500);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;

                    try
                    {
                        result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            ReternResult = cmd.Parameters["@Result"].Value.ToString();
                        }
                        trans.Commit();
                    }
                    catch (Exception EX)
                    {
                        trans.Rollback();
                    }
                    finally
                    {
                        trans.Dispose();
                        con.Close();
                    }
                }

            }

            return ReternResult;
        }
    }
}
