using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using KitapeviStokTakipWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitapeviStokTakipWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService authService;
        public AuthController(IAuthService _authService)
        {
            this.authService = _authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public string Register([FromBody] User user)
        {
            return authService.RegisterUser(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await authService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "bad" });

            return Ok(user);
        }
    }
}