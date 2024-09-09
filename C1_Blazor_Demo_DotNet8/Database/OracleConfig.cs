using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using static C1_Blazor_Demo_DotNet8.Database.OracleConfig;
using System.Runtime.Serialization;

namespace C1_Blazor_Demo_DotNet8.Database
{
    public class OracleConfig : SqlMapper.IDynamicParameters
    {
        public List<ExecuationParameter> Params { get; set; }
        private readonly DynamicParameters dynamicParameters = new DynamicParameters();
        private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();

        public OracleConfig()
        {
            Params = new List<ExecuationParameter>();
        }

        // Method to add an Oracle parameter with optional size and value
        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, object value = null, int? size = null)
        {
            OracleParameter oracleParameter;

            // If size is provided, use it in the OracleParameter constructor
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

        // Overload method to add an Oracle parameter without value or size
        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, direction);
            oracleParameters.Add(oracleParameter);
        }

        // Method to attach parameters to the Oracle command
        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            if (command is OracleCommand oracleCommand)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }

        // Method to retrieve a parameter value by name from Params
        public dynamic Get(string name)
        {   
            const string dbType = "Oracle";

            if (Params == null)
            {
                throw new ArgumentNullException(nameof(Params), "Params collection cannot be null.");
            }

            if (dbType == "Oracle")
            {
                var param = Params.FirstOrDefault(p => p.ParamName == name);
                return param?.Value?.Value ?? param?.Value;
            }
            else
            {
                var param = Params.FirstOrDefault(p => p.ParamName == "@" + name);
                return param?.Value;
            }
        }

        // Method to retrieve the list of Oracle parameters
        public IEnumerable<OracleParameter> GetOracleParameters()
        {
            return oracleParameters;
        }

        // Method to retrieve the output value of a specific parameter by name
        public string GetOutputValue(string paramName)
        {
            var outputParam = oracleParameters.FirstOrDefault(p => p.ParameterName == paramName && p.Direction == ParameterDirection.Output);
            return outputParam?.Value?.ToString();
        }

        // Class representing execution parameters for the database operations
        public class ExecuationParameter
        {
            public string? ParamName { get; set; }
            public dynamic Value { get; set; }
            public ParameterDirection Direction { get; set; }
            public int Size { get; set; }
        }
    }

    // Helper class to handle database configurations
    public class DbFormatter
    {
        private readonly IConfiguration _config;
        string DataBase;

        public List<ExecuationParameter> Params { get; set; }

        public static string _type { get; set; }

        public DbFormatter(IConfiguration config)
        {
            Params = new List<ExecuationParameter>();
            _config = config;
            DataBase = _config.GetSection("DBServer").Value;
            _type = DataBase;
        }
    }
}
