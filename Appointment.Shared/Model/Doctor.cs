using System.Collections.Generic;

namespace Appointment.Shared.Model
{
    public class Doctor
    {
        public Doctor()
        {
            DoctorMedicalProcedures = new List<DoctorMedicalProcedure>();
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<DoctorMedicalProcedure> DoctorMedicalProcedures { get; set; }
    }
}