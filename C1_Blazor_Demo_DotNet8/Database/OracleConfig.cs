using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
namespace C1_Blazor_Demo_DotNet8.Database
{
    public class OracleConfig : SqlMapper.IDynamicParameters
    {
        public List<ExecuationParameter> Params { get; set; }
        private readonly DynamicParameters dynamicParameters = new DynamicParameters();
        private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, object value = null, int? size = null)
        {
            OracleParameter oracleParameter;
            if (size.HasValue)
            {
                oracleParameter = new OracleParameter(name, oracleDbType, size.Value, value, direction);
            }
            else
            {
                oracleParameter = new OracleParameter(name, oracleDbType, value, direction);
            }

            oracleParameters.Add(oracleParameter);
        }

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, direction);
            oracleParameters.Add(oracleParameter);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            var oracleCommand = command as OracleCommand;

            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }

        public dynamic Get(string name)
        {

            var param = Params.Where(p => p.ParamName == name).FirstOrDefault();
            if (param != null)
            {
                if (param.Value == null)
                {
                    return param.Value;
                }
                else
                {
                    return param.Value.Value;
                }
            }
            return null;
        }
        public class ExecuationParameter
        {
            public string ParamName { get; set; }
            public dynamic Value { get; set; }
            public ParameterDirection Direction { get; set; }

            public int Size { get; set; }
        }
    }
}
