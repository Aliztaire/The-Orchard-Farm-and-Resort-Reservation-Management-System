using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OrchardFarmRMS.DataAccess
{
    internal class UserAccess
    {
        private readonly string _connectionString;

        public UserAccess()
        {
            // Connection String 
            _connectionString = ConfigurationManager.ConnectionStrings["OrchardFarmDB"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'OrchardFarmDB' not found in configuration.");
        }

        // Returns true if credentials match in db
        public string ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || (password is null)) return null;

            const string sql = "SELECT UserRole FROM dbo.Users WHERE Username = @u AND Password = @p";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar, 100) { Value = username });
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar, 256) { Value = password });

            conn.Open();
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value) return null;
            return result.ToString();
        }
    }
}
