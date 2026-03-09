using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
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
            string query = """
                           INSERT INTO tasks (text, completed, createdAt)
                           VALUES (@text, @completed, @createdAt)
                           """;

            using var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(query, task);
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
                           SELECT * FROM tasks
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
