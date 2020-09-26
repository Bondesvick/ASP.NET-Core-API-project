using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProject.DTOs;

namespace WebApiProject.Services
{
    public interface ILoginServices
    {
        public Task<LoggedInUser> SignIn(UserToLogin aUser);
    }
}