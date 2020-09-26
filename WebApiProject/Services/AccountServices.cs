using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApiProject.DTOs;
using WebApiProject.Models;

namespace WebApiProject.Services
{
    public class AccountServices : IAccountServices
    {
        public UserManager<ApplicationUser> UserManager { get; }

        public AccountServices(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="aUser">object with user details to create</param>
        /// <returns>Returned result</returns>
        public async Task<IdentityResult> CreateUser(UserToRegister aUser)
        {
            if (aUser == null)
                throw new NullReferenceException("Register Model is null");

            if (aUser.PassWord != aUser.ConfirmPassWord)
                throw new ArgumentException("Passwords do not match");

            var user = new ApplicationUser();
            user.FirstName = aUser.FirstName;
            user.LastName = aUser.LastName;
            user.UserName = aUser.Email;
            user.Photo = aUser.Photo;
            user.Email = aUser.Email;

            var result = await UserManager.CreateAsync(user, aUser.PassWord);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user, "Admin");
            }

            return result;
        }

        /// <summary>
        /// Updates user information
        /// </summary>
        /// <param name="email">email of user to update</param>
        /// <param name="aUser">new user information to update</param>
        /// <returns>The returned result</returns>
        public async Task<IdentityResult> UpdateUser(string email, UserToUpdate aUser)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.FirstName = aUser.FirstName;
                user.LastName = aUser.LastName;
                user.Photo = aUser.Photo;
                user.Email = aUser.Email;

                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return result;
                }

                throw new ApplicationException("Unable to update user");
            }

            throw new ApplicationException("User not found");
        }

        /// <summary>
        /// Delete a user profile
        /// </summary>
        /// <param name="email">email of user to delete</param>
        /// <returns>The returned response</returns>
        public async Task<IdentityResult> DeleteUser(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return result;
                }

                throw new Exception("Unable to delete user");
            }

            throw new Exception("User not found");
        }

        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email">email of user to search</param>
        /// <returns>The returned response</returns>
        public async Task<UserToReturn> GetUserByEmail(string email)
        {
            var result = await UserManager.FindByEmailAsync(email);
            if (result != null)
            {
                var user = new UserToReturn()
                {
                    LastName = result.LastName,
                    FirstName = result.FirstName,
                    Email = result.Email,
                    Photo = result.Photo,
                    DateCreated = result.DateCreated
                };

                return user;
            }
            throw new Exception("User not found");
        }

        /// <summary>
        /// Fetch all users i  pages
        /// </summary>
        /// <param name="page">page of user to show</param>
        /// <returns>The returned response</returns>
        public AllUsersToReturn GetAllUsers(int page)
        {
            var result = UserManager.Users;

            if (result.Any())
            {
                var users = new List<UserToReturn>();

                foreach (var item in result)
                {
                    var user = new UserToReturn
                    {
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        Email = item.Email,
                        Photo = item.Photo,
                        DateCreated = item.DateCreated
                    };
                    users.Add(user);
                }

                int toSkip = (page - 1) * 5;
                int remainder = users.Count - toSkip;

                if (remainder >= 5)
                {
                    //return users.Skip(toSkip).Take(5).ToList();
                    return new AllUsersToReturn
                    {
                        CurrentPage = page,
                        Users = users.Skip(toSkip).Take(5).ToList()
                    };
                }
                else if (!(remainder < 1))
                {
                    return new AllUsersToReturn
                    {
                        CurrentPage = page,
                        Users = users.Skip(toSkip).Take(remainder).ToList()
                    };
                    //return users.Skip(toSkip).Take(remainder).ToList();
                }
            }
            throw new Exception("No user not found");
        }
    }
}