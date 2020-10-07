using System;
using System.Net.Http;
using Appointment.Shared.Model;
using Appointment.Web.Areas.Identity;
using Appointment.Web.Data;
using Appointment.Web.Data.Interfaces;
using Appointment.Web.Model;
using Appointment.Web.Model.Email;
using Appointment.Web.Model.Validation;
using Appointment.Web.Model.ViewModels;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;

namespace Appointment.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddHttpContextAccessor();

            services.AddScoped<IUserService, UserService>();
            
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped(s => new AppSettingsModel(Configuration.GetSection("AppSettings")));
            services.AddScoped<IPasswordGenerator, RandomPasswordGenerator>();

            services.AddHttpClient<IAppointmentService, AppointmentService>(c =>
                {
                    c.BaseAddress = new Uri(Configuration["AppSettings:WebApiUrl"]);
                });
            
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<HttpClient>();
            
            //Fluent Validator
            // services.AddTransient<IValidator<AppointmentViewModel>, AppointmentViewModelValidator>();
            services.AddTransient<IValidator<AppointmentViewModel>>(x => new AppointmentViewModelValidator(x.GetRequiredService<IAppointmentService>()));

            services.AddTransient<IValidator<UserViewModel>, UserViewModelValidator>();

            //Radzen setup
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
            
            //Automapper
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            DatabaseInitializer.SeedData(userManager, roleManager);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
