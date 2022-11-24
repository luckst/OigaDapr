using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OigaDpr.RegisterApi.Infrastructure.NamedExceptions;
using OigaDpr.RegisterApi.Models;
using OigaDpr.RegisterApi.Services;

namespace OigaDpr.RegisterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username) ||
                    string.IsNullOrEmpty(user.FirstName) ||
                    string.IsNullOrEmpty(user.LastName))
                    return BadRequest();

                var result = await _registerService.AddUser(user);

                if (result < 0) return BadRequest("Error Adding the user");

                return Ok(result);
            }
            catch (UsernameTakenException)
            {
                return StatusCode(409, "Username already taken");
            }
        }
    }
}
