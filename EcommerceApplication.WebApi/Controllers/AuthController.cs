
using EcommerceApplication.Application.Services;
using EcommerceApplication.Application.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApplication.WebApi.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthController : ControllerBase
    {
        public readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _userRepository.LoginAsync(request)) ;
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.LogoutAsync();
            return Ok("process is success");

        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {

            return Ok(await _userRepository.CreateUserAsync());
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser()
        {
            return Ok();
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            return Ok();
        }



    }
}
