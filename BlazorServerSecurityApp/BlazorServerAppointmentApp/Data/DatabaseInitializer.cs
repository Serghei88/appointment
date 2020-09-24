using System.Collections.Generic;
using BlazorServerAppointmentApp.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BlazorServerAppointmentApp.Data
{
    public class DatabaseInitializer
    {
        public static void SeedData
        (UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedDoctorsAndMedicalProcedures(dbContext);
        }

        public static void SeedDoctorsAndMedicalProcedures(ApplicationDbContext dbContext)
        {
            if (dbContext.MedicalProcedures.Count() != 0)
            {
                return;
            }

            var procedure1 = new MedicalProcedure()
                {Name = "Medical Procedure 1", Description = "Description of the procedure 1"};
            var procedure2 = new MedicalProcedure()
                {Name = "Medical Procedure 2", Description = "Description of the procedure 2"};
            var procedure3 = new MedicalProcedure()
                {Name = "Medical Procedure 3", Description = "Description of the procedure 3"};
            var procedure4 = new MedicalProcedure()
                {Name = "Medical Procedure 4", Description = "Description of the procedure 4"};
            var procedure5 = new MedicalProcedure()
                {Name = "Medical Procedure 5", Description = "Description of the procedure 5"};
            dbContext.MedicalProcedures.AddRange(procedure1, procedure2, procedure3, procedure4, procedure5);

            dbContext.SaveChanges();

            dbContext.Doctors.AddRange(
                new Doctor
                {
                    FirstName = "John", LastName = "Smith",
                    MedicalProcedures = new List<MedicalProcedure>()
                    {
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 1")
                    }
                },
                new Doctor
                {
                    FirstName = "Adam", LastName = "Sandler",
                    MedicalProcedures = new List<MedicalProcedure>()
                    {
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 2")
                    }
                },
                new Doctor
                {
                    FirstName = "Brad", LastName = "Pitt",
                    MedicalProcedures = new List<MedicalProcedure>()
                    {
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 1"),
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 2"),
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 3"),
                    }
                },
                new Doctor
                {
                    FirstName = "Abraham", LastName = "Lincoln",
                    MedicalProcedures = new List<MedicalProcedure>()
                    {
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 4")
                    }
                },
                new Doctor
                {
                    FirstName = "Angelina", LastName = "Jolie",
                    MedicalProcedures = new List<MedicalProcedure>()
                    {
                        dbContext.MedicalProcedures.FirstOrDefault(x => x.Name == "Medical Procedure 5")
                    }
                }
            );
            dbContext.SaveChanges();
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