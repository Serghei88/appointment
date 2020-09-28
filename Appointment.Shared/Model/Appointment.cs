using System;

namespace Appointment.Shared.Model
{
    public class Appointment
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; }
        public MedicalProcedure MedicalProcedure { get; set; }
    }
}