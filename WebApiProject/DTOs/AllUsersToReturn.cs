using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProject.DTOs
{
    public class AllUsersToReturn
    {
        public int CurrentPage { get; set; }
        public List<UserToReturn> Users { get; set; }
    }
}