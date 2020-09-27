namespace Appointment.Shared.DTO
{
    public class DoctorMedicalProcedureDTO
    {
        public long DoctorId { get; set; }
        public DoctorDTO Doctor { get; set; }
 
        public long MedicalProcedureId { get; set; }
        public MedicalProcedureDTO MedicalProcedure { get; set; }
    }
}