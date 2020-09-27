using System.Collections.Generic;
using System.Linq;
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
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DoctorsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _context.Doctors.Include(x=> x.DoctorMedicalProcedures)
                .ThenInclude(x=>x.MedicalProcedure).FirstOrDefaultAsync(a => a.Id == id);
            
            return Ok(new DoctorDTO()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                DoctorMedicalProcedures = doctor.DoctorMedicalProcedures.Select(x=> 
                    new DoctorMedicalProcedureDTO()
                    {
                        DoctorId = x.DoctorId,
                        MedicalProcedureId = x.MedicalProcedureId,
                        MedicalProcedure = new MedicalProcedureDTO()
                        {
                            Id = x.MedicalProcedure.Id,
                            Name = x.MedicalProcedure.Name,
                            Description = x.MedicalProcedure.Description
                        }
                    }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(DoctorDTO doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            _context.Add(doctor);
            await _context.SaveChangesAsync();
            return Ok(doctor.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DoctorDTO doctorDto)
        {
            var doctor = _context.Doctors.Include(x=>x.DoctorMedicalProcedures).FirstOrDefault(x => x.Id == doctorDto.Id);
            var medicalProcedureIds = doctorDto.DoctorMedicalProcedures.Select(x => x.MedicalProcedureId).ToList();
            
            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            
            var proceduresToRemove = doctor.DoctorMedicalProcedures
                .Where(x => !medicalProcedureIds.Contains(x.MedicalProcedureId)).ToList();
            foreach (var doctorMedicalProcedure in proceduresToRemove)
            {
                doctor.DoctorMedicalProcedures.Remove(doctorMedicalProcedure);
            }


            foreach (var medicalProcedureId in medicalProcedureIds
                .Where(medicalProcedureId => doctor.DoctorMedicalProcedures.All(x => x.MedicalProcedureId != medicalProcedureId)))
            {
                doctor.DoctorMedicalProcedures.Add(new DoctorMedicalProcedure()
                {
                    DoctorId = doctor.Id,
                    MedicalProcedureId = medicalProcedureId
                });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = new Doctor() {Id = id};
            _context.Remove(doctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}