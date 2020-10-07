using System;
using System.ComponentModel.DataAnnotations;

namespace Appointment.Web.Model.ViewModels
{
    public class AppointmentViewModel
    {
        public AppointmentViewModel()
        {
            User = new UserViewModel();
        }
        
        public long Id { get; set; }
        public UserViewModel User { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public long MedicalProcedureId { get; set; }

        public MedicalProcedureViewModel MedicalProcedure { get; set; }
    }
}