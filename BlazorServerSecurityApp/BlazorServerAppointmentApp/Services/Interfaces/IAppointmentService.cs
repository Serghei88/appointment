using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorServerAppointmentApp.Model;

namespace BlazorServerAppointmentApp.Data.Interfaces
{
    public interface IAppointmentService
    {
        public Task<bool> CheckAppointmentAvailability(AppointmentViewModel appointmentViewModel);
        public Task CreateAppointment(AppointmentViewModel appointmentViewModel);

        public Task<List<DoctorViewModel>> GetDoctors();
        public Task<DoctorViewModel> GetDoctor(long Id);
        public Task CreateOrUpdateDoctor(DoctorViewModel doctor);

        public Task DeleteDoctor(long doctorId);
        public Task<List<MedicalProcedureViewModel>> GetMedicalProcedures();
        public Task<MedicalProcedureViewModel> GetMedicalProcedure(long Id);

        public Task CreateOrUpdateMedicalProcedure(MedicalProcedureViewModel medicalProcedureViewModel);
        public Task DeleteMedicalProcedure(long medicalProcedureId);
        public Task<List<AppointmentViewModel>> GetAppointments();
        public Task<List<AppointmentViewModel>> GetUserAppointments(string userId);

    }
}