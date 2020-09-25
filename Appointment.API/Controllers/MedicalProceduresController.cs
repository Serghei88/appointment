using System.Threading.Tasks;
using Appointment.Shared.DTO;
using Appointment.Shared.Model;
using AutoMapper;
using BlazorServerAppointmentApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalProceduresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public MedicalProceduresController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var medicalProcedures = await _context.MedicalProcedures.ToListAsync();
            return Ok(medicalProcedures);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var medicalProcedure = await _context.MedicalProcedures.FirstOrDefaultAsync(a=>a.Id ==id);
            return Ok(medicalProcedure);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(MedicalProcedureDTO medicalProcedureDto)
        {
            var medicalProcedure = _mapper.Map<MedicalProcedure>(medicalProcedureDto);
            _context.Add(medicalProcedure);
            await _context.SaveChangesAsync();
            return Ok(medicalProcedure.Id); 
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(MedicalProcedureDTO medicalProcedureDto)
        {
            var procedureDto = _mapper.Map<MedicalProcedureDTO>(medicalProcedureDto);
            _context.Entry(procedureDto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalProcedure = new MedicalProcedure() { Id = id };
            _context.Remove(medicalProcedure);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}