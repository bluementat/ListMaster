using ListMaster.Server.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Data
{
    public class UserInitializer
    {
        public async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
            await SeedRoleClaims(roleManager, context);
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {          
            IdentityResult adminRoleResult;
            IdentityResult subscriberRoleResult;

            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            bool subscriberRoleExists = await roleManager.RoleExistsAsync("User");

            if (!adminRoleExists)
            {
                adminRoleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!subscriberRoleExists)
            {
                subscriberRoleResult = await roleManager.CreateAsync(new IdentityRole("User"));
            }

        }

        private async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@ListMaster").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin";
                user.NormalizedUserName = "ADMIN";
                user.Email = "admin@ListMaster";
                user.EmailConfirmed = true;

                IdentityResult result = await userManager.CreateAsync(user, "password");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        private async Task SeedRoleClaims(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            IdentityRole role = roleManager.FindByNameAsync("Admin").Result;
            if(!(context.RoleClaims.Any(c => c.ClaimValue == "AddAdministrator")))
            {
                context.RoleClaims.Add(new IdentityRoleClaim<string>
                {
                    ClaimType = "Permission",
                    ClaimValue = "AddAdministrator",
                    RoleId = role.Id,
                });
            }            

            role = roleManager.FindByNameAsync("User").Result;
            if (!(context.RoleClaims.Any(c => c.ClaimValue == "AddUser")))
            {
                context.RoleClaims.Add(new IdentityRoleClaim<string>
                {
                    ClaimType = "Permission",
                    ClaimValue = "AddUser",
                    RoleId = role.Id,
                });
            }            

            await context.SaveChangesAsync();
        }
        
    }
}
