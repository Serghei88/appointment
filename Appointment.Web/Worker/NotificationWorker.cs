using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Web.Data;
using Appointment.Web.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Appointment.Web.Worker
{
    public class NotificationWorker : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IMapper _mapper;
        private DateTime lastRun;
        private IEmailSender _emailSender;
        private IAppointmentService _appointmentService;
        private readonly IServiceProvider _serviceProvider;


        public NotificationWorker(IEmailSender emailSender, IAppointmentService appointmentService,
            IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _appointmentService = appointmentService;
            _serviceProvider = serviceProvider;
            lastRun = DateTime.Now;
        }

        private async void DoWork(object state)
        {
            // wait API to start
            Thread.Sleep(5000);
            // Create a new scope to retrieve scoped services
            using var scope = _serviceProvider.CreateScope();
            // Get the DbContext instance
            var dbcontxt = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var appointments = await _appointmentService.GetAppointments();
            var nextDayAppointments =
                appointments.Where(x => x.Time < DateTime.Today.AddDays(2) &&
                                        x.Time > DateTime.Today.AddDays(1));
            foreach (var appointment in nextDayAppointments)
            {
                var user = dbcontxt.ApplicationUser.FirstOrDefault(x => x.Id == appointment.User.Id);
                if (user != null)
                {
                    await _emailSender.SendEmailAsync(user.Email, "You have an appointment for Tomorrow",
                        $"Hello {user.FirstName} {user.LastName}" +
                        $"You have an appointment for {appointment.Time}");
                }
            }

            lastRun = DateTime.Now;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}