using C1_Blazor_Demo_DotNet8.Models.ViewModels;
using Dapper;

using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace C1_Blazor_Demo_DotNet8.Repository
{
    public class NFT_AuthRepository
    {

        private readonly IConfiguration _configuration;

        public NFT_AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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
                param.Add("p_log_id", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                param.Add("p_tab_log_id", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

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

                            objNFTlog.LogId = param.Get<string>("p_log_id");
                            objNFTlog.LogTableId = param.Get<string>("p_tab_log_id");



                            return objNFTlog;
                        }

                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                        finally
                        {
                            if (objNFTlog.LogId != null)
                            {
                                AddNFT_Log_val(objNFTlog.LogTabId, objNFTlog.LogId, Table_name, "", "", "1");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding NFT Log.", ex);
            }
        }
        public async Task<NftAuthLogModel> AddNFT_Log_val(string log_tab_id, string log_id, string table_name, string old_value,
                                                            string new_value, string rework_flag)
        {
            NftAuthLogModel objNFTlog = new NftAuthLogModel();

            try
            {
                var param = new DynamicParameters();

                param.Add("p_log_tab_id", log_tab_id, DbType.String, ParameterDirection.Input);
                param.Add("p_log_id", log_id ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_table_name", table_name ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_old_value", old_value ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_new_value", new_value ?? "", DbType.String, ParameterDirection.Input);
                param.Add("p_rework_flag", rework_flag ?? "", DbType.String, ParameterDirection.Input);

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
                            var query2 = "ABS_auth_skb.nft_auth_log_val_i";

                            await conn.ExecuteAsync(query2, param: param, commandType: CommandType.StoredProcedure, transaction: transaction);

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
                throw new Exception("Error adding NFT Log Value.", ex);
            }
        }
        public IDbConnection GetConnection()
        {
            var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("Database").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }
    }

}
