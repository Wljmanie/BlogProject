using BlogProject.Data;
using BlogProject.Enums;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Create DB from the Migration
            await _dbContext.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));   
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_dbContext.Users.Any())
            {
                return;
            }

            var adminUser = new BlogUser()
            {
                Email = "wljmanie@gmail.com",
                UserName = "wljmanie@gmail.com",
                FirstName = "Wesley",
                LastName = "Manie",
                PhoneNumber = "(012) 345-6789",
                EmailConfirmed = true
            };
            
            await _userManager.CreateAsync(adminUser, "Cas&123");
          
            
               // await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            var modUser = new BlogUser()
            {
                Email = "WesMan@gmail.com",
                UserName = "WesMan@gmail.com",
                FirstName = "WesMod",
                LastName = "ManMod",
                PhoneNumber = "(012) 345-6789",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(modUser, "Cas&123");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
