using Appointment.Shared.DTO;
using Appointment.Shared.Model;
using AutoMapper;
using BlazorServerAppointmentApp.Model;

namespace BlazorServerAppointmentApp.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<DoctorDTO, DoctorViewModel>().ReverseMap();
            CreateMap<MedicalProcedureDTO, MedicalProcedureViewModel>().ReverseMap();
            CreateMap<AppointmentDTO, AppointmentViewModel>().ReverseMap();
            CreateMap<DoctorMedicalProcedureDTO, DoctorMedicalProcedureViewModel>().ReverseMap();
        }
    }
}