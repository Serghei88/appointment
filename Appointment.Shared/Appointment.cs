using System;

namespace BlazorServerAppointmentApp.Model
{
    public class Appointment
    {
        public long Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Time { get; set; }
        public MedicalProcedure MedicalProcedure { get; set; }
    }
}