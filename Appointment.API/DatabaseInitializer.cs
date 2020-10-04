using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Appointment.API;
using Appointment.Shared.Model;

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

            var doctor1 = new Doctor {FirstName = "John", LastName = "Smith"};
            var doctor2 = new Doctor {FirstName = "Adam", LastName = "Sandler"};
            var doctor3 = new Doctor {FirstName = "Brad", LastName = "Pitt",};
            var doctor4 = new Doctor {FirstName = "Abraham", LastName = "Lincoln",};
            var doctor5 = new Doctor {FirstName = "Angelina", LastName = "Jolie",};
            
            dbContext.Doctors.AddRange(doctor1, doctor2, doctor3, doctor4, doctor5);
            dbContext.SaveChanges();
            
            doctor1.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor1.Id, MedicalProcedureId = procedure1.Id
            });
            doctor2.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor2.Id, MedicalProcedureId = procedure2.Id
            });
            doctor3.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor3.Id, MedicalProcedureId = procedure3.Id
            });
            doctor4.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor4.Id, MedicalProcedureId = procedure4.Id
            });
            doctor5.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor5.Id, MedicalProcedureId = procedure5.Id
            });
            doctor3.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor3.Id, MedicalProcedureId = procedure4.Id
            });
            doctor3.DoctorMedicalProcedures.Add( new DoctorMedicalProcedure()
            {
                DoctorId = doctor3.Id, MedicalProcedureId = procedure5.Id
            });
            dbContext.SaveChanges();

        }
    }
}