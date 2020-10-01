using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Appointment.Shared.Model;
using BlazorServerAppointmentApp.Data.Interfaces;
using BlazorServerAppointmentApp.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace BlazorServerAppointmentApp.Data
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private IEmailSender _emailSender;
        private IPasswordGenerator _passwordGenerator;
        private NavigationManager _navigationManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserService> _logger;


        public UserService(UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            IPasswordGenerator passwordGenerator,
            NavigationManager navigationManager, SignInManager<ApplicationUser> signInManager,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _passwordGenerator = passwordGenerator;
            _navigationManager = navigationManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<string> CreateUser(UserViewModel userViewModel)
        {
            var user = new ApplicationUser
            {
                UserName = userViewModel.Email,
                Email = userViewModel.Email,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName
            };
            var password = _passwordGenerator.GeneratePassword();

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = $"Identity/Account/ConfirmEmail?userId={user.Id}&code={code}&returnUrl=~/";

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by " +
                    $"<a href='{_navigationManager.BaseUri + HtmlEncoder.Default.Encode(callbackUrl.ToString())}'>clicking here</a>." +
                    $"Your automatically generated password: {password}");

                if (!_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            return user.Id;
        }
    }
}