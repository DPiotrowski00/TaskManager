using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TaskManagerAPI.models;

namespace TaskManagerAPI.services
{
    public class TaskSqlService(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task CreateTask(TaskModel task)
        {
            string idfetcher = """
                               SELECT id FROM logindata WHERE username = @username
                               """;
            
            string query = """
                           INSERT INTO tasks (text, completed, createdAt, creatorid)
                           VALUES (@text, @completed, @createdAt, @id)
                           """;

            using var connection = CreateConnection();
            try
            {
                var id = await connection.QuerySingleAsync<int>(idfetcher, new { username = task.creator });
                await connection.ExecuteAsync(query, new { task.text, task.completed, task.createdAt, id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public async Task<List<TaskModel>> GetTasks()
        {
            string query = """
                           SELECT t.id, t.text, t.completed, t.createdAt, t.creatorid, l.username as creator FROM tasks t JOIN logindata l ON l.id = t.creatorid
                           """;

            using var connection = CreateConnection();
            try
            {
                return [.. await connection.QueryAsync<TaskModel>(query)];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public async Task UpdateTask(TaskModel task)
        {
            string query = """
                           UPDATE tasks SET completed = @completed WHERE id = @id
                           """;

            using var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(query, new {task.completed, task.id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public async Task DeleteTask(int id)
        {
            string query = """
                           DELETE FROM tasks WHERE id = @id
                           """;

            using var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(query, new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
