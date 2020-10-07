using System.Threading.Tasks;
using Appointment.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Repositories
{
    public class MedicalProcedureRepository: Repository<MedicalProcedure, int>, IMedicalProcedureRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalProcedureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MedicalProcedure> Get(long key)
        {
            return await _context.MedicalProcedures.FirstOrDefaultAsync(x => x.Id == key);
        }
    }
}