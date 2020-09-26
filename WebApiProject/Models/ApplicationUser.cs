using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApiProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Max characters allowed is 30")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Max characters allowed is 30")]
        public string FirstName { get; set; }

        public string Photo { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}