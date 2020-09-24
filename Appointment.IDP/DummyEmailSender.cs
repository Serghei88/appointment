using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Appointment.IDP
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}