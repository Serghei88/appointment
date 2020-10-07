using System;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Web.Data.Interfaces;
using Appointment.Web.Model.ViewModels;
using FluentValidation;

namespace Appointment.Web.Model.Validation
{
    public class AppointmentViewModelValidator :AbstractValidator<AppointmentViewModel>
    {
        private IAppointmentService _appointmentService;
        public AppointmentViewModelValidator(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            RuleFor(x => x.Time).NotEmpty()
                .WithMessage("You need to select an appointment time.")
                .MustAsync(CheckAppointmentAvailability)
                .WithMessage(x=>  $"Appointment for selected procedure at {x.Time} not available" );

            RuleFor(x => x.MedicalProcedureId).NotEqual(0)
                .WithMessage("Please select medical procedure");
            
            RuleFor(x => x.User).SetValidator(new UserViewModelValidator());
        }
        
        private async Task<bool> CheckAppointmentAvailability(AppointmentViewModel appointmentViewModel, 
            DateTime time, CancellationToken arg2)
        {
            return await _appointmentService.CheckAppointmentAvailability(appointmentViewModel)
                .ConfigureAwait(false);
        }
    }
}