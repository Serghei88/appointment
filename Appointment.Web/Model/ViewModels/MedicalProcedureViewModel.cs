using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Appointment.Web.Model.ViewModels
{
    public class MedicalProcedureViewModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Description is too long.")]

        public string Description { get; set; }
        public IList<DoctorMedicalProcedureViewModel> DoctorMedicalProcedures { get; set; }
    }
}