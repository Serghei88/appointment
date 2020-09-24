using System;
using Appointment.IDP.Data;
using BlazorServerAppointmentApp.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Appointment.IDP.Areas.Identity.IdentityHostingStartup))]

namespace Appointment.IDP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserDbContextConnection")));


                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                        options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();

                services.AddTransient<IEmailSender, DummyEmailSender>();
            });
        }
    }
}