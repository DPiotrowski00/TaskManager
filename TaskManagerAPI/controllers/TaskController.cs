using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagerAPI.models;
using TaskManagerAPI.services;

namespace TaskManagerAPI.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController(TaskSqlService service) : ControllerBase
    {
        private readonly TaskSqlService _sqlService = service;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel task)
        {
            try
            {
                task.createdAt = DateTime.Now;

                await _sqlService.CreateTask(task);

                return Ok(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TaskModel>> GetTasks()
        {
            try
            {
                return Ok(await _sqlService.GetTasks());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<TaskModel>> UpdateTask([FromBody] TaskModel task)
        {
            try
            {
                await _sqlService.UpdateTask(task);
                return Ok(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NoContent();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                await _sqlService.DeleteTask(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
