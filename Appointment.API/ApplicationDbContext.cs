﻿using Appointment.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorMedicalProcedure> DoctorMedicalProcedures { get; set; }
        public DbSet<MedicalProcedure> MedicalProcedures { get; set; }
        public DbSet<Appointment.Shared.Model.Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorMedicalProcedure>()
                .HasKey(t => new { t.DoctorId, t.MedicalProcedureId });
 
            modelBuilder.Entity<DoctorMedicalProcedure>()
                .HasOne(sc => sc.Doctor)
                .WithMany(s => s.DoctorMedicalProcedures)
                .HasForeignKey(sc => sc.DoctorId);
 
            modelBuilder.Entity<DoctorMedicalProcedure>()
                .HasOne(sc => sc.MedicalProcedure)
                .WithMany(c => c.DoctorMedicalProcedures)
                .HasForeignKey(sc => sc.MedicalProcedureId);
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}