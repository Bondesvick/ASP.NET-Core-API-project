using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApiProject.Data;

namespace WebApiProject.Models
{
    public class PreSeeder
    {
        public static async Task Seeder(AppDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            ctx.Database.EnsureCreated();

            if (!roleManager.Roles.Any())
            {
                var listOfRoles = new List<IdentityRole>
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("Customer")
                };

                foreach (var role in listOfRoles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var listOfUsers = new List<ApplicationUser>
                {
                    new ApplicationUser{ UserName="cidos@me.com", Email = "cidos@me.com", LastName="Ibe", FirstName="Francis", Photo = "~/images/avarta.jpg" },
                    new ApplicationUser{ UserName="iamnwizu@me.com", Email = "iamnwizu@me.com", LastName="Nwizu", FirstName="Kaosili", Photo = "~/images/avarta.jpg" }
                };

                int counter = 0;
                foreach (var user in listOfUsers)
                {
                    var result = await userManager.CreateAsync(user, "P@$$word1");

                    if (result.Succeeded)
                    {
                        if (counter == 0)
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, "Customer");
                        }
                    }
                    counter++;
                }
            }
        }
    }
}