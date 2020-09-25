using System.Collections.Generic;

namespace Appointment.Shared.DTO
{
    public class DoctorDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<MedicalProcedureDTO> MedicalProcedures { get; set; }
    }
}