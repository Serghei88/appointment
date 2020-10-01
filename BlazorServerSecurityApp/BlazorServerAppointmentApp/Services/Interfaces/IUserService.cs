using System;
using System.Threading.Tasks;
using BlazorServerAppointmentApp.Model;

namespace BlazorServerAppointmentApp.Data.Interfaces
{
    public interface IUserService
    {
        public Task<String> CreateUser(UserViewModel userViewModel);
    }
}