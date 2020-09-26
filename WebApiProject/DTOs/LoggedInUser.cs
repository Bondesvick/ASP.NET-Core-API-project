using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.DTOs
{
    public class LoggedInUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}