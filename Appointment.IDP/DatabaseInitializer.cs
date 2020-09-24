using System;
using System.Linq;
using System.Security.Claims;
using BlazorServerAppointmentApp.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Appointment.IDP
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
            var jack = userManager.FindByNameAsync("Jack").Result;
            if (jack == null)
            {
                jack = new ApplicationUser
                {
                    UserName = "Jack",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(jack, "P@ssword1").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(jack, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Jack Torrance"),
                    new Claim(JwtClaimTypes.GivenName, "Jack"),
                    new Claim(JwtClaimTypes.FamilyName, "Torrance"),
                    new Claim(JwtClaimTypes.Email, "jack.torrance@email.com"),
                    new Claim("country", "BE")
                }).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(jack,
                        "Administrator").Wait();
                }

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var wendy = userManager.FindByNameAsync("Wendy").Result;
            if (wendy == null)
            {
                wendy = new ApplicationUser
                {
                    UserName = "Wendy",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(wendy, "P@ssword1").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(wendy, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Wendy Torrance"),
                    new Claim(JwtClaimTypes.GivenName, "Wendy"),
                    new Claim(JwtClaimTypes.FamilyName, "Torrance"),
                    new Claim(JwtClaimTypes.Email, "wendy.torrance@email.com"),
                    new Claim("country", "NL")
                }).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(wendy,
                        "User").Wait();
                }

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
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