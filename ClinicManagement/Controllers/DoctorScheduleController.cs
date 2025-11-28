using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Models;
using ClinicManagement.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.DAL.UnitOfWork;
using ClinicManagement.DTOs.DoctorScheduleRequests;
using Microsoft.AspNetCore.Authorization;

namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller for managing doctor schedules (weekly availability).
    /// Admin: define/modify; Receptionist: view schedules for appointments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all actions
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorScheduleController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance for repository access and transaction management.</param>
        public DoctorScheduleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/DoctorSchedule
        /// <summary>
        /// Gets all doctor schedules.
        /// </summary>
        /// <returns>A list of all doctor schedules.</returns>
        // GET: api/DoctorSchedule
        [HttpGet]
        [Authorize(Roles = "Admin")] // Only Admin views all schedules
        public async Task<ActionResult<IEnumerable<DoctorScheduleAllDto>>> GetAll()
        {
            var schedules = await _unitOfWork.DoctorSchedules.GetAllAsync();

            var result = schedules.Select(s => new DoctorScheduleAllDto
            {
                Id = s.Id,
                DoctorId = s.DoctorId,
                DoctorName = s.Doctor?.FullName,// If navigation property is available
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Gets a specific doctor schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the doctor schedule.</param>
        /// <returns>The doctor schedule matching the provided ID.</returns>
        // GET: api/DoctorSchedule/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin views specific schedule details
        public async Task<ActionResult<DoctorScheduleDetailDto>> GetById(int id)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            var dto = new DoctorScheduleDetailDto
            {
                Id = schedule.Id,
                DoctorId = schedule.DoctorId,
                DayOfWeek = schedule.DayOfWeek,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                DoctorName = schedule.Doctor?.FullName // Doctor Name for a partiular doctor schedule
            };

            return Ok(dto);
        }

        /// <summary>
        /// Gets all schedules for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The doctor's unique identifier.</param>
        /// <returns>A list of schedules for the specified doctor.</returns>
        // GET: api/DoctorSchedule/ByDoctor/5
        [HttpGet("ByDoctor/{doctorId}")]
        [Authorize(Roles = "Admin,Receptionist")] // Receptionist needs schedules for booking
        public async Task<ActionResult<IEnumerable<DoctorScheduleByDoctorDto>>> GetByDoctor(int doctorId)
        {
            var schedules = await _unitOfWork.DoctorSchedules.GetByDoctorIdAsync(doctorId);

            var dtoList = schedules.Select(s => new DoctorScheduleByDoctorDto
            {
                Id = s.Id,
                DoctorId = s.DoctorId,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                DoctorName = s.Doctor?.FullName
            });

            return Ok(dtoList);
        }

        /// <summary>
        /// Creates a new doctor schedule.
        /// </summary>
        /// <param name="schedule">The doctor schedule to create.</param>
        /// <returns>A response indicating the result of the create operation, along with the created schedule.</returns>
        // POST: api/DoctorSchedule
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only Admin defines schedules
        public async Task<ActionResult> Create([FromBody] CreateDoctorScheduleDto dto)
        {
            var schedule = new DoctorSchedule
            {
                DoctorId = dto.DoctorId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _unitOfWork.DoctorSchedules.AddAsync(schedule);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = schedule.Id }, schedule);
        }

        /// <summary>
        /// Updates an existing doctor schedule.
        /// </summary>
        /// <param name="id">The ID of the schedule to update.</param>
        /// <param name="updatedSchedule">The updated doctor schedule data.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        // PUT: api/DoctorSchedule/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin modifies schedules
        public async Task<ActionResult> Update(int id, UpdateDoctorScheduleDto dto)
        {

            var existingSchedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (existingSchedule == null)
                return NotFound();

            // Validate that the DoctorId exists
            var doctorExists = await _unitOfWork.Doctors.ExistsAsync(dto.DoctorId);
            if (!doctorExists)
            {
                return BadRequest($"Doctor with ID {dto.DoctorId} does not exist.");
            }

            // Update properties
            existingSchedule.DayOfWeek = dto.DayOfWeek;
            existingSchedule.StartTime = dto.StartTime;
            existingSchedule.EndTime = dto.EndTime;
            existingSchedule.DoctorId = dto.DoctorId;

            _unitOfWork.DoctorSchedules.Update(existingSchedule);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific doctor schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        // DELETE: api/DoctorSchedule/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin deletes schedules
        public async Task<ActionResult> Delete([FromBody] DeleteDoctorScheduleDto dto)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(dto.Id);
            if (schedule == null)
                return NotFound();

            _unitOfWork.DoctorSchedules.Remove(schedule);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
