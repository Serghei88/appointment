using System.Collections.Generic;

namespace BlazorServerAppointmentApp.Model
{
    public class Doctor
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<MedicalProcedure> MedicalProcedures { get; set; }
    }
}