using System.Threading.Tasks;
using Appointment.API.Repositories;
using Appointment.Shared.DTO;
using Appointment.Shared.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalProceduresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalProceduresController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var medicalProcedures =
                await _unitOfWork.MedicalProcedureRepository.GetAll().ToListAsync();
            return Ok(medicalProcedures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var medicalProcedure = await _unitOfWork.MedicalProcedureRepository.GetAll()
                .FirstOrDefaultAsync(a => a.Id == id);
            return Ok(medicalProcedure);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MedicalProcedureDTO medicalProcedureDto)
        {
            var medicalProcedure = _mapper.Map<MedicalProcedure>(medicalProcedureDto);
            await _unitOfWork.MedicalProcedureRepository.AddAsync(medicalProcedure);
            _unitOfWork.Complete();
            return Ok(medicalProcedure.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(MedicalProcedureDTO medicalProcedureDto)
        {
            var procedure = _mapper.Map<MedicalProcedure>(medicalProcedureDto);
            await _unitOfWork.MedicalProcedureRepository.UpdateAsync(procedure);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalProcedure = new MedicalProcedure() {Id = id};
            await _unitOfWork.MedicalProcedureRepository.DeleteAsync(medicalProcedure);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}