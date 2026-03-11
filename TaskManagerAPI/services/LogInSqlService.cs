using Dapper;
using Microsoft.AspNetCore.Identity;
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
                           SELECT password FROM logindata WHERE username = BINARY @username
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

        private async Task<bool> checkIfUsernameExists(string username)
        {
            string query = @"
                           SELECT 
                           CASE
                           WHEN EXISTS(SELECT 1 from logindata WHERE username = BINARY @username)
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

        public async Task<bool> createUser(string username, string passwordHash)
        {
            string query = @"
                           INSERT INTO logindata (username, password) VALUES (@username, @passwordHash)
                           ";

            var connection = CreateConnection();
            try
            {
                if(!await checkIfUsernameExists(username))
                {
                    await connection.ExecuteAsync(query, new { username, passwordHash });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<int> getUserId(string username)
        {
            string query = @"
                           SELECT id FROM logindata WHERE username = @username
                           ";

            var connection = CreateConnection();
            try
            {
                return await connection.QuerySingleAsync<int>(query, new { username });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
