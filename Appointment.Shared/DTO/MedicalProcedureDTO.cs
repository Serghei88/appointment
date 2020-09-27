using System.Collections.Generic;

namespace Appointment.Shared.DTO
{
    public class MedicalProcedureDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<DoctorMedicalProcedureDTO> DoctorMedicalProcedures { get; set; }
    }
}