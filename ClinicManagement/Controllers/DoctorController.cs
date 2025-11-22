using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Models;
using ClinicManagement.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller responsible for managing Doctor entities.
    /// Provides endpoints for CRUD operations on doctors in the clinic system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _unitOfWork.Doctors.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Retrieves a specific doctor by id asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>An <see cref="IActionResult"/> containing the doctor if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The new <see cref="Doctor"/> entity to add.</param>
        /// <returns>An <see cref="IActionResult"/> with the created doctor’s data and location.</returns>
        // POST: api/doctor
        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != doctor.Id)
                return BadRequest("Doctor ID mismatch.");

            var existingDoctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (existingDoctor == null)
                return NotFound();

            await _unitOfWork.Doctors.UpdateDoctorAsync(doctor);
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
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var existingDoctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (existingDoctor == null)
                return NotFound();

            await _unitOfWork.Doctors.DeleteDoctorAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();

        }
    }

}