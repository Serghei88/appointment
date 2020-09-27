namespace Appointment.Shared.Model
{
    public class DoctorMedicalProcedure
    {
        public long DoctorId { get; set; }
        public Doctor Doctor { get; set; }
 
        public long MedicalProcedureId { get; set; }
        public MedicalProcedure MedicalProcedure { get; set; }
    }
}