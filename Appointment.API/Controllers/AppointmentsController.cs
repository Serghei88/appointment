using System.Linq;
using System.Threading.Tasks;
using Appointment.API.Repositories;
using Appointment.Shared.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalProcedureRepository _medicalProcedureRepository;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentRepository appointmentRepository, 
            IMedicalProcedureRepository medicalProcedureRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _medicalProcedureRepository = medicalProcedureRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentRepository.GetAll()
                .Include(x => x.MedicalProcedure).ToListAsync();
            return Ok(appointments);
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckAvailability(AppointmentDTO appointmentDto)
        {
            return Ok(await _appointmentRepository.CheckAvailability(appointmentDto.MedicalProcedure.Id,appointmentDto.Time));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_appointmentRepository.Get(id));
        }

        [HttpGet("userappointments/{id}")]
        public async Task<IActionResult> GetUserAppointments(string id)
        {
            
            return Ok(await _appointmentRepository.GetUserAppointments(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AppointmentDTO appointmentDto)
        {
            var medicalProcedure = await _medicalProcedureRepository.Get(appointmentDto.MedicalProcedure.Id);
            var appointment = new Shared.Model.Appointment()
            {
                Time = appointmentDto.Time,
                UserId = appointmentDto.UserId,
                MedicalProcedure = medicalProcedure
            };
            await _appointmentRepository.UpdateAsync(appointment);
            return Ok(appointment.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentRepository.Get(id);
            await _appointmentRepository.DeleteAsync(appointment);
            return NoContent();
        }
    }
}