using CoinMarketCap.Business.Abstract;
using CoinMarketCap.Business.Concrete.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoinMarketCap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(new UserDto
            {
                Id = userToLogin.Data.Id,
                FirstName = userToLogin.Data.FirstName,
                LastName = userToLogin.Data.LastName,
                Email = userToLogin.Data.Email,
            });

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (registerResult.Success)
            {
                var result = _authService.CreateAccessToken(new UserDto { 
                    Id = registerResult.Data.Id,
                    FirstName = registerResult.Data.FirstName,
                    LastName = registerResult.Data.LastName,
                    Email = registerResult.Data.Email,
                });

                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result);
            }

            return BadRequest(registerResult);
        }
    }
}
