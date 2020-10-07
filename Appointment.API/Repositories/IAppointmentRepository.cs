using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointment.API.Repositories
{
    public interface IAppointmentRepository: IRepository<Shared.Model.Appointment, long>
    {
        Task<Shared.Model.Appointment> Get(long key);
        Task<List<Shared.Model.Appointment>> GetUserAppointments(string userId);
        Task<bool> CheckAvailability(long medicalProcedureId, DateTime time);
    }
}