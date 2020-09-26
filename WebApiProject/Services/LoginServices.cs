using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiProject.DTOs;
using WebApiProject.Models;

namespace WebApiProject.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginServices(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoggedInUser> SignIn(UserToLogin aUser)
        {
            var result = await _userManager.FindByEmailAsync(aUser.Email);

            if (result != null)
            {
                var confirmed = await _userManager.CheckPasswordAsync(result, aUser.Password);

                if (!confirmed)
                {
                    throw new NullReferenceException("Wrong Password");
                }

                await _signInManager.PasswordSignInAsync(result, aUser.Password, true, true);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Id),
                    new Claim(ClaimTypes.Name, result.LastName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credential
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(securityTokenDescriptor);

                var myToken = tokenHandler.WriteToken(token);

                var user = new LoggedInUser
                {
                    LastName = result.LastName,
                    FirstName = result.FirstName,
                    Email = result.Email,
                    Photo = result.Photo,
                    Token = myToken,
                    ExpiryDate = securityTokenDescriptor.Expires
                };

                return user;
            }
            throw new NullReferenceException("User not found");
        }
    }
}