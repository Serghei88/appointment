using System.Collections.Generic;

namespace BlazorServerAppointmentApp.Model
{
    public class DoctorViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<MedicalProcedureViewModel> MedicalProcedures { get; set; }
    }
}