using Application.DTOs;
using Application.Service;
using Application.Service_Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Application.Service_Interface.IAuthenticationService;

namespace API.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.RegisterAsync(registerDto);

            if (result.IsAuthenticated)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.LoginAsync(loginDto);

            if (result.IsAuthenticated)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetCount")]
        public async Task<IActionResult> GetCountAllAsync()
        {
            return Ok(await _authenticationService.GetCountOfUsers());
        }

    }
}
