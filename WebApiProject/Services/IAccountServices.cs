using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApiProject.DTOs;
using WebApiProject.Models;

namespace WebApiProject.Services
{
    public interface IAccountServices
    {
        public Task<IdentityResult> CreateUser(UserToRegister aUser);

        public Task<IdentityResult> UpdateUser(string email, UserToUpdate aUser);

        public Task<IdentityResult> DeleteUser(string email);

        public Task<UserToReturn> GetUserByEmail(string email);

        public AllUsersToReturn GetAllUsers(int page);
    }
}