using Appointment.Shared.DTO;
using Appointment.Shared.Model;
using Appointment.Web.Model;
using Appointment.Web.Model.ViewModels;
using AutoMapper;

namespace Appointment.Web.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<DoctorDTO, DoctorViewModel>().ReverseMap();
            CreateMap<MedicalProcedureDTO, MedicalProcedureViewModel>().ReverseMap();
            CreateMap<AppointmentDTO, AppointmentViewModel>()
                .ForMember(dest => dest.User,
                opt => 
                    opt.MapFrom(src => new UserViewModel(){Id = src.UserId.ToString()}))
                .ForMember(dest => dest.MedicalProcedureId,
                    opt => 
                        opt.MapFrom(src => src.MedicalProcedure.Id));
            CreateMap<AppointmentViewModel, AppointmentDTO>()
                .ForMember(dest =>
                        dest.UserId,
                    opt => 
                        opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.MedicalProcedure,
                    opt => 
                        opt.MapFrom(src =>new MedicalProcedureViewModel(){Id=src.MedicalProcedureId}));
            CreateMap<DoctorMedicalProcedureDTO, DoctorMedicalProcedureViewModel>().ReverseMap();
        }
    }
}