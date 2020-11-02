using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayComplete.Persistence
{
    public static class DataSeedingInitializer
    {
        public static async Task UserAndRoleSeeding(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Admin
            if (userManager.FindByEmailAsync("rafaelgoldberg@paycomplete.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "rafaelgoldberg@paycomplete.com",
                    Email = "rafaelgoldberg@paycomplete.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Admin123*").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            //Create Manager
            if (userManager.FindByEmailAsync("Manager@paycomplete.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "Manager@paycomplete.com",
                    Email = "Manager@paycomplete.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Manager123*").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            //Create Staff User
            if (userManager.FindByEmailAsync("User@paycomplete.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "User@paycomplete.com",
                    Email = "User@paycomplete.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "User123*").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            //Create No Role User
            if (userManager.FindByEmailAsync("PlainUser@paycomplete.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "PlainUser@paycomplete.com",
                    Email = "PlainUser@paycomplete.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "PlainUser123*").Result;
              

                // No Role Assigned
            }
        }
    }
}
