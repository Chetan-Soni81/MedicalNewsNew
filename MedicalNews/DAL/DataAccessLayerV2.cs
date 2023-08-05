using System.Data.SqlClient;
using System.Data;

namespace MedicalNews.DAL
{
    public class DataAccessLayerV2
    {
        #region Methods
        #region GlobalVariables
        public SqlConnection _connection;
        public string? StringConnection;
        #endregion

        private async Task OpenConnection()
        {
            if (_connection == null)
            {
                //connection strings
                //StringConnection = "Server=MSI\\SQLEXPRESS; Database=MedicalNews; Trusted_connection=True;";
                //StringConnection = "server=SG2NWPLS19SQL-v07.mssql.shr.prod.sin2.secureserver.net; database=medicalnews; uid =medical; pwd=News#$%123; pooling=true; min pool size=0; max pool size=100";
                _connection = new SqlConnection(StringConnection ?? ConnectionString());
            }

            if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
            {
                await _connection.OpenAsync();
            }
        }

        private string ConnectionString()
        {
            //StringConnection = "server=SG2NWPLS19SQL-v07.mssql.shr.prod.sin2.secureserver.net; database=medicalnews; uid =medical; pwd=News#$%123; pooling=true; min pool size=0; max pool size=100";
            StringConnection = "Server=MSI\\SQLEXPRESS; Database=MedicalNews; Trusted_connection=True;";
            return StringConnection;
        }

        private async Task<SqlCommand> CreateCommand(string command, SqlParameter[]? parameters)
        {
            await OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = command;

            if (parameters != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
            }

            return cmd;
        }

        public SqlCommand CreateTransCmd(string procName, SqlTransaction trans, SqlParameter[]? param)
        {
            SqlCommand cmd = new SqlCommand(procName, _connection, trans);
            try
            {
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _connection;

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
                _connection.Close();
                return cmd;
            }
        }
        #endregion

        public async Task<DataTable> SelectRecordByDatatable(string procName)
        {
            using (SqlCommand cmd = await CreateCommand(procName, null))
            {
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    return dt;
                }
            }
        }

        public async Task<DataTable> SelectRecordByDatatable(string procName, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = await CreateCommand(procName, parameters))
            {
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    return dt;
                }
            }
        }

        public async Task<DataSet> SelectRecordByDataSet(string procName)
        {
            using (SqlCommand cmd = await CreateCommand(procName, null))
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
                        catch (Exception ex)
                        {
                            return new DataSet();
                        }
                        return ds;
                    }
                }
            }
        }

        public async Task<DataSet> SelectRecordByDataSet(string procName, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = await CreateCommand(procName, parameters))
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
                        catch (Exception ex)
                        {
                            return new DataSet();
                        }
                        return ds;
                    }
                }
            }
        }

        public async Task<SqlDataReader> SelectRecordBydataReader(string procName)
        {
            using (SqlCommand cmd = await CreateCommand(procName, null))
            {
                SqlDataReader? sdr = null;
                return sdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }

        public async Task<SqlDataReader> SelectRecordByDataReader(string procName, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = await CreateCommand(procName, parameters))
            {
                SqlDataReader? sdr = null;
                return sdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }

        public async Task<int> ExecuteNonQuery(string CommandName, SqlParameter[] pars)
        {
            int result = 0;
            SqlTransaction trans;
            ConnectionString();
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();

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
                        result = await cmd.ExecuteNonQueryAsync();
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
        public async Task<int> ExecuteNonQueryWithOutPutParameter(string procName, SqlParameter[]? ps)
        {
            int i = 0;
            using (SqlCommand cmd = await CreateCommand(procName, ps))
            {
                try
                {
                    i = Convert.ToInt32(cmd.ExecuteScalarAsync());
                }
                catch (Exception ex)
                {
                    return 0;
                }
                return i;
            }
        }

        public async Task<int> ExecuteNonQueryParamIntTemp(string CommandName, SqlParameter[] pars)
        {
            int result = 0;

            SqlTransaction trans;
            ConnectionString();
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();

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
                        result = await cmd.ExecuteNonQueryAsync();
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


        public async Task<string> ExecuteNonQueryParamStringTemp(string CommandName, SqlParameter[] pars)
        {
            int result = 0;
            string ReternResult = "0";
            SqlTransaction trans;
            ConnectionString();
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                if (con.State != ConnectionState.Open)
                {
                    await con.OpenAsync();

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
                        result = await cmd.ExecuteNonQueryAsync();
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
