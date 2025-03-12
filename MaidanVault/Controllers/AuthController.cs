using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MaidanVault_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHomeService _homeService;

        public AuthController(IUserService userService, IHomeService homeService)
        {
            _userService = userService;
            _homeService = homeService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var message = await _userService.SignUpAsync(user);
            return Ok(new { message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var response = await _userService.LoginAsync(loginRequest);
                if (response.Count() > 1)
                    return Ok(new { Message = response[0], Token = response[1], User = response[2] }); // msg, token, userid
                else
                    return BadRequest(new { Message = response[0] }); // msg, token, userid
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Something went wrong please try again!" });
            }
        }
    }

}
