using System.Linq;
using System.Threading.Tasks;
using Appointment.Shared.DTO;
using AutoMapper;
using BlazorServerAppointmentApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Appointment = Appointment.Shared.Model.Appointment;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public AppointmentsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return Ok(appointments);
        }
        
        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckAvailability(AppointmentDTO appointmentDto)
        {
            var appointments = await _context.Appointments
                .Where(x=> x.MedicalProcedure.Id == appointmentDto.MedicalProcedure.Id &&
                           x.Time == appointmentDto.Time)
                .CountAsync();
            var doctorMedicalPrCount = await _context.DoctorMedicalProcedures
                .Where(x => x.MedicalProcedureId == appointmentDto.MedicalProcedure.Id).CountAsync();
            
            return Ok(appointments < doctorMedicalPrCount);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a=>a.Id ==id);
            return Ok(appointment);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(AppointmentDTO appointmentDto)
        {
            var appointment = new Shared.Model.Appointment()
            {
                Time = appointmentDto.Time,
                UserId = appointmentDto.UserId,
                MedicalProcedure = _context.MedicalProcedures.FirstOrDefault(x=>x.Id == appointmentDto.MedicalProcedure.Id)
            };
            _context.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(appointment.Id); 
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(AppointmentDTO appointmentDto)
        {
            var appointment = _mapper.Map<Shared.Model.Appointment>(appointmentDto);
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = new Shared.Model.Appointment() { Id = id };
            _context.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}