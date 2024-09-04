using C1_Blazor_Demo_DotNet8.Database;
using C1_Blazor_Demo_DotNet8.Models.Entities;
using C1_Blazor_Demo_DotNet8.Models.ViewModels;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace C1_Blazor_Demo_DotNet8.Repository
{
    public class DashboardRepository
    {
 
            private readonly IConfiguration _configuration;

            public DashboardRepository(IConfiguration configuration)
            {
                _configuration = configuration;
            }


            public async Task<List<object>> GetReworkdata(string Log_id)
            {
            List<object> result = new List<object>();

            try
            {
                var Param = new OracleConfig();

                Param.Add("p_log_id", OracleDbType.NVarchar2, ParameterDirection.Input, Log_id.ToString());

                Param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "Abs_Auth_Skb.Fetch_Rework_Data";

                    result = (await SqlMapper.QueryAsync<object>(conn, query, param: Param, commandType: CommandType.StoredProcedure)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
            }
        public async Task<List<UserAccountModel>> GetPendingFuncNM(string UserID, string BranchID)
        {
            List<UserAccountModel> result = new List<UserAccountModel>();

            try
            {
                var Param = new OracleConfig();

                Param.Add("p_user_id", OracleDbType.NVarchar2, ParameterDirection.Input, UserID.ToString());
                Param.Add("p_branch_id", OracleDbType.NVarchar2, ParameterDirection.Input, BranchID.ToString());

                Param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "Abs_Auth_Skb.Fetch_Rework_func_NM";

                    result =(await SqlMapper.QueryAsync<UserAccountModel>(conn, query, param: Param, commandType: CommandType.StoredProcedure)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<List<UserAccountModel>> GetRequestQueue(string UserID, string BranchID,string FunctionID)
        {
            List<UserAccountModel> result = new List<UserAccountModel>();

            try
            {
                var Param = new OracleConfig();

                Param.Add("p_user_id", OracleDbType.NVarchar2, ParameterDirection.Input, UserID.ToString());
                Param.Add("p_branch_id", OracleDbType.NVarchar2, ParameterDirection.Input, BranchID.ToString());
                Param.Add("p_function_id", OracleDbType.NVarchar2, ParameterDirection.Input, FunctionID.ToString());
                Param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "Abs_Auth_Skb.GetRequestQueue";

                    result = (await SqlMapper.QueryAsync<UserAccountModel>(conn, query, param: Param, commandType: CommandType.StoredProcedure)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public async Task<object> getUsers(string UserID, string BranchID)
            {
                object result = null;
                try
                {
                    var Param = new OracleConfig();

                    Param.Add("p_user_id", OracleDbType.NVarchar2, ParameterDirection.Input, UserID.ToString());
                    Param.Add("p_branch_id", OracleDbType.NVarchar2, ParameterDirection.Input, BranchID.ToString());

                    Param.Add("p_result", OracleDbType.RefCursor, ParameterDirection.Output);

                    var conn = this.GetConnection();
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        var query = "Abs_Auth_Skb.FetchUserDetails";

                        result = SqlMapper.Query(conn, query, param: Param, commandType: CommandType.StoredProcedure);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return result;

            }


        public async Task<string> send_back(int log_id,string branch_id, string remarks)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("p_log_ID", log_id, DbType.String, ParameterDirection.Input);
                param.Add("p_remarks", remarks, DbType.String, ParameterDirection.Input);
                param.Add("pbranch_id", branch_id, DbType.String, ParameterDirection.Input);
               

                using (var conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            var query = "ABS_auth_skb.send_back_auth";

                           await conn.ExecuteAsync(query, param: param, commandType: CommandType.StoredProcedure, transaction: transaction);

                            transaction.Commit();

                         
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                   
                }
                return "Okay";

            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer.", ex);
            }
        }




        //Part of Monim
        public async Task<CustomerModel> AddCustomer(CustomerModel objCust)
        {
            string errorCode = string.Empty;
            string errorMsg = string.Empty;
            string customer_ID = string.Empty;
            try
            {
                var param = new DynamicParameters();

                param.Add("pcustomer_id", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                param.Add("pcustomer_category_id", "I", DbType.String, ParameterDirection.Input);
                param.Add("pcustomer_short_nm", objCust.CustomerShortName ?? "", DbType.String, ParameterDirection.Input);
                param.Add("pcustomer_salutation", objCust.CustomerSalutation ?? "", DbType.String, ParameterDirection.Input);
                param.Add("pcustomer_first_nm", objCust.CustomerFirstName ?? "", DbType.String, ParameterDirection.Input);
                param.Add("pcustomer_middle_nm", objCust.CustomerMiddleName ?? "", DbType.String, ParameterDirection.Input);
                param.Add("pcustomer_last_nm", objCust.CustomerLastName ?? "", DbType.String, ParameterDirection.Input);
                param.Add("puser_id", objCust.MakeBy ?? "", DbType.String, ParameterDirection.Input);
                param.Add("perr_code", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                param.Add("perr_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                using (var conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            var query = "ABS_auth_skb.cor_cus_profile_i";

                            await conn.ExecuteAsync(query, param: param, commandType: CommandType.StoredProcedure, transaction: transaction);

                            transaction.Commit();

                             customer_ID = param.Get<string>("pcustomer_id");
                            customer_ID=customer_ID.PadLeft(10, '0');
                            errorCode = param.Get<string>("perr_code");
                            errorMsg = param.Get<string>("perr_msg");

                            if (!string.IsNullOrEmpty(errorCode))
                            {
                                throw new ApplicationException($"Error: {errorCode}, Message: {errorMsg}");
                            }
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                    if (!string.IsNullOrEmpty(errorCode))
                    {
                        throw new ApplicationException($"Error: {errorCode}, Message: {errorMsg}");
                    }
                    else
                    {
                        AddNFT_Log("0031", "1052", "ADD", objCust.MakeBy, "5", "cor_cus_profile_mnm", "Customer Information", "CUSTOMER_ID", "Customer Id", customer_ID, "1");
                    }
                }
                return objCust;

            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer.", ex);
            }
        }
        public async Task<NftAuthLogModel> AddNFT_Log(string BranchId, string FunctionId, string Last_Action, string UserId,
           string Auth_level, string Table_name, string Table_Display_name, string Pk_column_name, string Pk_column_display,
           string Pk_column_value, string Parent_table_flag)
        {
            NftAuthLogModel objNFTlog = new NftAuthLogModel();

            try
            {
                var param = new DynamicParameters();

                param.Add("p_branchid", BranchId, DbType.String, ParameterDirection.Input);
                param.Add("p_function_id", FunctionId ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_action_status", Last_Action ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_remarks", "", DbType.String, ParameterDirection.Input);
                param.Add("p_make_by", UserId ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_auth_level", Auth_level ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_auth_status_id", "", DbType.String, ParameterDirection.Input);
                param.Add("p_auth_by", "", DbType.String, ParameterDirection.Input);
                param.Add("p_auth_pending_level", "", DbType.String, ParameterDirection.Input);
                param.Add("p_table_name", Table_name ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_table_display_name", Table_Display_name ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_pk_column_name", Pk_column_name ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_pk_display_name", Pk_column_display ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_pk_column_value", Pk_column_value, DbType.String, ParameterDirection.Input);
                param.Add("p_parent_table_flag", Parent_table_flag ?? "", DbType.String, ParameterDirection.Input);

                using (var conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            var query = "ABS_auth_skb.nft_auth_log_i";

                            await conn.ExecuteAsync(query, param: param, commandType: CommandType.StoredProcedure, transaction: transaction);

                            transaction.Commit();

                            return objNFTlog;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding NFT Log.", ex);
            }
        }

        //Rumman Code

        public List<ABS_SYS_APP_FUNCTION> get_list_by_funcID()
        {

            List<ABS_SYS_APP_FUNCTION> result = new List<ABS_SYS_APP_FUNCTION>();



            try
            {
                var Param = new OracleConfig();

                Param.Add("res", OracleDbType.RefCursor, ParameterDirection.Output);
                Param.Add("P_ERROR_MSG", OracleDbType.NVarchar2, ParameterDirection.Output, size: 32000);
                Param.Add("P_ERROR_CODE", OracleDbType.NVarchar2, ParameterDirection.Output, size: 32000);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "PG_NFT_AUTHORIZATION.get_unauth_data_by_funcID";

                    result = SqlMapper.Query<ABS_SYS_APP_FUNCTION>(conn, query, param: Param, commandType: CommandType.StoredProcedure).ToList();


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public List<AUTH_LOG> get_auth_log_list(string functionID)
        {

            List<AUTH_LOG> result = new List<AUTH_LOG>();



            try
            {
                var Param = new OracleConfig();

                Param.Add("p_function_id", OracleDbType.NVarchar2, ParameterDirection.Input, functionID);
                Param.Add("p_auth_log_list", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "PG_NFT_AUTHORIZATION.get_auth_log_list";

                    result = SqlMapper.Query<AUTH_LOG>(conn, query, param: Param, commandType: CommandType.StoredProcedure).ToList();


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<AUTH_LOG> get_log_details_extended(string log_id)
        {

            List<AUTH_LOG> result = new List<AUTH_LOG>();



            try
            {
                var Param = new OracleConfig();

                Param.Add("p_log_id", OracleDbType.NVarchar2, ParameterDirection.Input, log_id);
                Param.Add("p_log_details", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "PG_NFT_AUTHORIZATION.get_log_details_extended";

                    result = SqlMapper.Query<AUTH_LOG>(conn, query, param: Param, commandType: CommandType.StoredProcedure).ToList();


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public IDbConnection GetConnection()
            {
                var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("Database").Value;
                var conn = new OracleConnection(connectionString);
                return conn;
            }
        
    }
}
