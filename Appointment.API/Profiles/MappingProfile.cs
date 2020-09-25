using Appointment.Shared.DTO;
using Appointment.Shared.Model;
using AutoMapper;

namespace Appointment.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();
            CreateMap<DoctorDTO, Doctor>().ReverseMap();
            CreateMap<MedicalProcedureDTO, MedicalProcedure>().ReverseMap();
            CreateMap<AppointmentDTO, Shared.Model.Appointment>().ForMember(x => x.ApplicationUser,
                    dest =>
                        dest.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}