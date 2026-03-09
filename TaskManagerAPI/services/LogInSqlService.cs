using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace TaskManagerAPI.services
{
    public class LogInSqlService (IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<string> getPasswordHash(string username)
        {
            string query = @"
                           SELECT password FROM logindata WHERE username = @username
                           ";

            var connection = CreateConnection();
            try
            {
                return await connection.QuerySingleAsync<string>(query, new { username });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public async Task<bool> checkIfUsernameExists(string username)
        {
            string query = @"
                           SELECT 
                           CASE
                           WHEN EXISTS(SELECT 1 from logindata WHERE username = @username)
                           THEN 1
                           ELSE 0
                           END
                           ";

            var connection = CreateConnection();
            try
            {
                return await connection.QuerySingleAsync<bool>(query, new { username });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        public async Task createUser(string username, string passwordHash)
        {
            string query = @"
                           INSERT INTO logindata (username, password) VALUES (@username, @passwordHash)
                           ";

            var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(query, new { username, passwordHash });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
