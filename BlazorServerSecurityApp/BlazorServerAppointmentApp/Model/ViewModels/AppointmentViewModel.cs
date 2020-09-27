using System;

namespace BlazorServerAppointmentApp.Model
{
    public class AppointmentViewModel
    {
        public AppointmentViewModel()
        {
            User = new UserViewModel();
        }
        
        public long Id { get; set; }
        public UserViewModel User { get; set; }
        public DateTime Time { get; set; }
        public long MedicalProcedureId { get; set; }
    }
}