using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Models;
using ClinicManagement.DAL.UnitOfWork;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClinicManagement.DTOs.PatientRequests;
using Serilog;
using Microsoft.AspNetCore.Authorization;


namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller for managing patient operations using Repository and Unit of Work Pattern.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All patient operations require authentication
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
        [Authorize(Roles = "Admin,Doctor,Receptionist")] // All roles can list patients
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();

            var result = patients.Where(p => !p.IsDeleted)
                                 .Select(p => new PatientAllDto
                                 {
                                     Id = p.Id,
                                     FirstName = p.FirstName,
                                     LastName = p.LastName,
                                     Email = p.Email,
                                     PhoneNumber = p.PhoneNumber,
                                     DateOfBirth = p.DateOfBirth
                                 }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Gets a patient by unique identifier if not deleted.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")] // All roles can view patient details
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return NotFound();

            var patientDto = new PatientDetailDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth
            };

            return Ok(patientDto);
        }

        /// <summary>
        /// Creates a new patient record.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")] // Receptionist creates patients, Admin has full control
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = DateTime.SpecifyKind(dto.DateOfBirth,DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
                // Appointments is initialized automatically to an empty list!
            };


            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Updates an existing patient record.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist")] // Admin full control, Receptionist manages patients
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto dto)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return NotFound();

            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.Email = dto.Email;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.DateOfBirth = DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Utc);

            await _unitOfWork.Patients.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Soft deletes a patient record (marks as deleted, does not remove from DB).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can soft-delete patients
        public async Task<IActionResult> SoftDeletePatient(int id,[FromBody] SoftDeletePatientDto dto)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return NotFound();

            patient.IsDeleted = true;

            Log.ForContext("LogType", "Deletion")
                .Information("Patient with ID {PatientId} marked as deleted at {DeletedAt}", patient.Id,dto.DeletedAt);



            await _unitOfWork.Patients.UpdateAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
