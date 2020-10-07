using System.Collections.Generic;
using Appointment.Web.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Appointment.Shared.Model;

namespace Appointment.Web.Data
{
    public class DatabaseInitializer
    {
        public static void SeedData
        (UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers
            (UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync
                ("user@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "user@localhost", Email = "user@localhost", FirstName = "Nancy", LastName = "Davolio",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "UserPassword123!@#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "User").Wait();
                }
            }


            if (userManager.FindByNameAsync
                ("admin@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@localhost", Email = "admin@localhost", FirstName = "Mark", LastName = "Smith",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "AdminPassword123!@#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles
            (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("User").Result)
            {
                IdentityRole role = new IdentityRole {Name = "User"};
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
                ("Administrator").Result)
            {
                IdentityRole role = new IdentityRole {Name = "Administrator"};
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}