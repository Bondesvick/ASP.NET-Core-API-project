using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiProject.DTOs;
using WebApiProject.Models;
using WebApiProject.Services;

namespace WebApiProject.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ILoginServices _loginServices;

        public LogInController(ILogger<AccountController> logger, ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        // Login/SignIn
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] UserToLogin aUser)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _loginServices.SignIn(aUser));
            }

            return NotFound();
        }

        //[HttpGet("SignOut/{email}")]
        //public async Task<IActionResult> SignOut(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    var result = _signInManager.SignOutAsync();

        //    return NotFound(result);
        //}
    }
}