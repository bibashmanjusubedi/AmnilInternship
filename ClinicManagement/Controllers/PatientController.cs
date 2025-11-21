using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Models;
using ClinicManagement.DAL.UnitOfWork;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller for managing patient operations using Repository and Unit of Work Pattern.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the list of all active patients (not deleted).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Gets a patient by unique identifier if not deleted.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        /// <summary>
        /// Creates a new patient record.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Updates an existing patient record.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient updatedPatient)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return NotFound();

            patient.FirstName = updatedPatient.FirstName;
            patient.LastName = updatedPatient.LastName;
            patient.Email = updatedPatient.Email;
            patient.PhoneNumber = updatedPatient.PhoneNumber;
            patient.DateOfBirth = updatedPatient.DateOfBirth;

            await _unitOfWork.Patients.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Soft deletes a patient record (marks as deleted, does not remove from DB).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeletePatient(int id)
        {
            await _unitOfWork.Patients.SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
