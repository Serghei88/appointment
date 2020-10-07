using System;
using System.Threading.Tasks;
using Appointment.Web.Model;
using Appointment.Web.Model.ViewModels;

namespace Appointment.Web.Data.Interfaces
{
    public interface IUserService
    {
        public Task<String> CreateUser(UserViewModel userViewModel);
    }
}