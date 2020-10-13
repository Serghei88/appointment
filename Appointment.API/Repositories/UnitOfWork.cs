using System;

namespace Appointment.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext _context, IAppointmentRepository _appointmentRepository,
            IMedicalProcedureRepository _medicalProcedureRepository)
        {
            context = _context;
            AppointmentRepository = _appointmentRepository;
            MedicalProcedureRepository = _medicalProcedureRepository;
        }
        
        public IAppointmentRepository AppointmentRepository { get; }
        public IMedicalProcedureRepository MedicalProcedureRepository { get; }
        
        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}