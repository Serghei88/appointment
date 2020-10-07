using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Appointment.Web.Model.ViewModels
{
    public class DoctorViewModel
    {
        public DoctorViewModel()
        {
            DoctorMedicalProcedures = new List<DoctorMedicalProcedureViewModel>();
        }
            
        public long Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Name is too long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Last Name is too long.")]

        public string LastName { get; set; }

        public IList<DoctorMedicalProcedureViewModel> DoctorMedicalProcedures { get; set; }
    }
}