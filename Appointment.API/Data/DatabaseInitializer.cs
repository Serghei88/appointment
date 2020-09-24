using System.Collections.Generic;
using BlazorServerAppointmentApp.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BlazorServerAppointmentApp.Data
{
    public class DatabaseInitializer
    {
        public static void SeedData
        (ApplicationDbContext dbContext)
        {
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
    }
}