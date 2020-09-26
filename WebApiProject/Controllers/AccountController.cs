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
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(ILogger<AccountController> logger,
            IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        // account/CreateUser
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserToRegister aUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountServices.CreateUser(aUser);

                return Ok(result);
            }

            return NotFound();
        }

        //account/UpdateUser/email
        [HttpPut("UpdateUser/{email}")]
        public async Task<IActionResult> UpdateUser(string email, UserToUpdate aUser)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountServices.UpdateUser(email, aUser));
            }

            return NotFound();
        }

        [HttpDelete("DeleteUser/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountServices.DeleteUser(email));
            }

            return NotFound();
        }

        //account/getUser/email
        [HttpGet("GetUser/{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountServices.GetUserByEmail(email));
            }

            return NotFound();
        }

        //account/GetAllUsers/page
        [HttpGet("GetAllUsers")]
        [HttpGet("GetAllUsers/{page}")]
        public IActionResult GetAllUsers(int page = 1)
        {
            if (ModelState.IsValid)
            {
                return Ok(_accountServices.GetAllUsers(page));
            }

            return NotFound();
        }
    }
}