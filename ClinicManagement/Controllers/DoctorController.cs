using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Models;
using ClinicManagement.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ClinicManagement.DTOs.DoctorRequests;
using Microsoft.AspNetCore.Authorization;

namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller responsible for managing Doctor entities.
    /// Provides endpoints for CRUD operations on doctors in the clinic system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all actions
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance to coordinate repository operations.</param>
        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the collection of doctors.</returns>
        // GET: api/doctor
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only Admin allowed
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _unitOfWork.Doctors.GetAllDoctorsAsync();
            var doctorsDtos = doctors.Select(d => new DoctorAllDto
            {
                Id = d.Id,
                FullName = d.FullName,
                Specialization = d.Specialization,
                Email = d.Email,
                Phone = d.Phone
            }).ToList();
            return Ok(doctorsDtos);
        }

        /// <summary>
        /// Retrieves a specific doctor by id asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>An <see cref="IActionResult"/> containing the doctor if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var doctorDto = new DoctorDetailDto
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialization = doctor.Specialization,
                Email = doctor.Email,
                Phone = doctor.Phone
            };
            return Ok(doctorDto);
        }

        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The new <see cref="Doctor"/> entity to add.</param>
        /// <returns>An <see cref="IActionResult"/> with the created doctor’s data and location.</returns>
        // POST: api/doctor
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDoctor([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialization = dto.Specialization,
                Email = dto.Email,
                Phone = dto.Phone
            };

            await _unitOfWork.Doctors.AddDoctorAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        /// <summary>
        /// Updates an existing doctor asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to update.</param>
        /// <param name="doctor">The updated <see cref="Doctor"/> entity.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the update result.</returns>
        // PUT : api/doctor/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingDoctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (existingDoctor == null)
                return NotFound();

            existingDoctor.FullName = dto.FullName;
            existingDoctor.Specialization = dto.Specialization;
            existingDoctor.Email = dto.Email;
            existingDoctor.Phone = dto.Phone;

            await _unitOfWork.Doctors.UpdateDoctorAsync(existingDoctor);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a doctor asynchronously by id.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the deletion result.</returns>
        // DELETE: api/doctor/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor([FromBody] DeleteDoctorDto dto)
        {
            var existingDoctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(dto.Id);
            if (existingDoctor == null)
                return NotFound();

            await _unitOfWork.Doctors.DeleteDoctorAsync(dto.Id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();

        }
    }

}