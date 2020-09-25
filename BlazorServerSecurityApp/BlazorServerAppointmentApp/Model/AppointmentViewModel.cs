using System;

namespace BlazorServerAppointmentApp.Model
{
    public class AppointmentViewModel
    {
        public long Id { get; set; }
        public UserViewModel User { get; set; }
        public DateTime Time { get; set; }
        public MedicalProcedureViewModel MedicalProcedure { get; set; }
    }
}