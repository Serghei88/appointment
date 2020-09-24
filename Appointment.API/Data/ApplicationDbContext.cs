using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorServerAppointmentApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerAppointmentApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalProcedure> MedicalProcedures { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Model.Appointment> Appointments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}