using Appointment.Web.Model.ViewModels;
using FluentValidation;

namespace Appointment.Web.Model.Validation
{
    public class UserViewModelValidator: AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Please provide first name");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Please provide last name");
            RuleFor(user => user.Email).NotEmpty().EmailAddress().MaximumLength(255)
                .WithMessage("Please provide email");
        }
    }
}