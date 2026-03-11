using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
    public class LogInController (LogInSqlService service, JwtService jwtService) : ControllerBase
    {
        private readonly LogInSqlService _logInSqlService = service;
        private readonly JwtService _jwtService = jwtService;

        private string hashPassword(string username, string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(username, password);
        }

        [HttpGet]
        [Route("hashgen")]
        public ActionResult<string> getHash([FromBody] LoginRequest request)
        {
            return Ok(hashPassword(request.Username, request.Password));
        }

        [HttpPost]
        public async Task<ActionResult<string>> ValidateLogIn([FromBody] LoginRequest request)
        {
            var dbPasswordHash = await _logInSqlService.getPasswordHash(request.Username);
            var id = await _logInSqlService.getUserId(request.Username);

            if (dbPasswordHash == "")
            {
                return BadRequest("");
            }

            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(request.Username, dbPasswordHash, request.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return Ok(_jwtService.generateToken(id, request.Username));
            }
            else
            {
                return Ok("");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<bool>> RegisterUser([FromBody] LoginRequest request)
        {
            var result = await _logInSqlService.createUser(request.Username, hashPassword(request.Username, request.Password));
            return Ok(result);
        }
    }
}
