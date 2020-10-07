using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Repositories
{
    public class AppointmentRepository : Repository<Shared.Model.Appointment, int>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Shared.Model.Appointment> Get(long id)
        {
            return _context.Appointments.Include(x => x.MedicalProcedure)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Shared.Model.Appointment>> GetUserAppointments(string userId)
        {
            return await _context.Appointments.Where(a => a.UserId == userId)
                .Include(x => x.MedicalProcedure).ToListAsync();
        }


        public async Task<bool> CheckAvailability(long medicalProcedureId, DateTime time)
        {
            if (medicalProcedureId == 0)
            {
                return false;
            }

            var appointments = await _context.Appointments
                .Where(x => x.MedicalProcedure.Id == medicalProcedureId &&
                            x.Time == time)
                .CountAsync();
            var doctorMedicalPrCount = await _context.DoctorMedicalProcedures
                .Where(x => x.MedicalProcedureId == medicalProcedureId).CountAsync();

            return appointments < doctorMedicalPrCount;
        }
    }
}