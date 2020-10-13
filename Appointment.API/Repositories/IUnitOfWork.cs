using System;

namespace Appointment.API.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository AppointmentRepository { get; }
        IMedicalProcedureRepository MedicalProcedureRepository { get; }
        int Complete();
    }
}