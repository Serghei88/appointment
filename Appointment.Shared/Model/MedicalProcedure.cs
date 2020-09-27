using System.Collections.Generic;

namespace Appointment.Shared.Model
{
    public class MedicalProcedure
    {
        public MedicalProcedure()
        {
            DoctorMedicalProcedures = new List<DoctorMedicalProcedure>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<DoctorMedicalProcedure> DoctorMedicalProcedures { get; set; }
        
    }
}