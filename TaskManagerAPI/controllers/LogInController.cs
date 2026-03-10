using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagerAPI.services;

namespace TaskManagerAPI.controllers
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class LogInController (LogInSqlService service) : ControllerBase
    {
        private readonly LogInSqlService _logInSqlService = service;

        [HttpGet]
        [Route("hashgen")]
        public ActionResult<string> getHash([FromBody] LoginRequest request)
        {
            var passwordHasher = new PasswordHasher<object>();
            var passwordHash = passwordHasher.HashPassword(request.Username, request.Password);

            return Ok(passwordHash);
        }

        [HttpPut]
        public ActionResult createUser([FromBody] LoginRequest request)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> ValidateLogIn([FromBody] LoginRequest request)
        {
            var dbPasswordHash = await _logInSqlService.getPasswordHash(request.Username);

            if (dbPasswordHash == "")
            {
                return BadRequest(false);
            }

            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(request.Username, dbPasswordHash, request.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
