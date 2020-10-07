using System.Threading.Tasks;
using Appointment.Shared.Model;

namespace Appointment.API.Repositories
{
    public interface IMedicalProcedureRepository: IRepository<MedicalProcedure, int>
    {
        Task<MedicalProcedure> Get(long key);
    }
}