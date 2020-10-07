namespace Appointment.Web.Model.ViewModels
{
    public class DoctorMedicalProcedureViewModel
    {
        public long DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }
 
        public long MedicalProcedureId { get; set; }
        public MedicalProcedureViewModel MedicalProcedure { get; set; }
    }
}