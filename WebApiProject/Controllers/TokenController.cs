using System;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiProject.DTOs;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // Token/GetToken
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] EmailDto model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            // Get claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.LastName)
            };

            // Get JWT secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            //Generate the signin credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Create Security Token descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            //Build token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create the token
            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            var toReturn = new
            {
                mytoken = tokenHandler.WriteToken(token)
            };

            return Ok(toReturn);
        }
    }
}