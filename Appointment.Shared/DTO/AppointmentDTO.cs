using System;

namespace Appointment.Shared.DTO
{
    public class AppointmentDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime Time { get; set; }
        public MedicalProcedureDTO MedicalProcedure { get; set; }
    }
}