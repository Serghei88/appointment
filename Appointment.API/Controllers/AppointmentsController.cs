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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a=>a.Id ==id);
            return Ok(appointment);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(AppointmentDTO appointmentDto)
        {
            var appointment = _mapper.Map<Shared.Model.Appointment>(appointmentDto);
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