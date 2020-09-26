using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.DTOs
{
    public class UserToRegister
    {
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public string Photo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string PassWord { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassWord { get; set; }
    }
}