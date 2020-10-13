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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAll()
                .Include(x => x.MedicalProcedure).ToListAsync();
            return Ok(appointments);
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckAvailability(AppointmentDTO appointmentDto)
        {
            return Ok(await _unitOfWork.AppointmentRepository.CheckAvailability(appointmentDto.MedicalProcedure.Id,appointmentDto.Time));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_unitOfWork.AppointmentRepository.Get(id));
        }

        [HttpGet("userappointments/{id}")]
        public async Task<IActionResult> GetUserAppointments(string id)
        {
            
            return Ok(await _unitOfWork.AppointmentRepository.GetUserAppointments(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AppointmentDTO appointmentDto)
        {
            var medicalProcedure = await _unitOfWork.MedicalProcedureRepository.Get(appointmentDto.MedicalProcedure.Id);
            var appointment = new Shared.Model.Appointment()
            {
                Time = appointmentDto.Time,
                UserId = appointmentDto.UserId,
                MedicalProcedure = medicalProcedure
            };
            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            _unitOfWork.Complete();
            return Ok(appointment.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.Get(id);
            await _unitOfWork.AppointmentRepository.DeleteAsync(appointment);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}