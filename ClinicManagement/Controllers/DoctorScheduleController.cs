using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Models;
using ClinicManagement.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.DAL.UnitOfWork;
using ClinicManagement.DTOs.DoctorScheduleRequests;

namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller for managing doctor schedules (weekly availability).
    /// Admin: define/modify; Receptionist: view schedules for appointments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<DoctorSchedule>> GetById(int id)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }

        /// <summary>
        /// Gets all schedules for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The doctor's unique identifier.</param>
        /// <returns>A list of schedules for the specified doctor.</returns>
        // GET: api/DoctorSchedule/ByDoctor/5
        [HttpGet("ByDoctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<DoctorSchedule>>> GetByDoctor(int doctorId)
        {
            var schedules = await _unitOfWork.DoctorSchedules.GetByDoctorIdAsync(doctorId);
            return Ok(schedules);
        }

        /// <summary>
        /// Creates a new doctor schedule.
        /// </summary>
        /// <param name="schedule">The doctor schedule to create.</param>
        /// <returns>A response indicating the result of the create operation, along with the created schedule.</returns>
        // POST: api/DoctorSchedule
        [HttpPost]
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
        public async Task<ActionResult> Update(int id, DoctorSchedule updatedSchedule)
        {
            if (id != updatedSchedule.Id)
                return BadRequest("ID mismatch");

            var existingSchedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (existingSchedule == null)
                return NotFound();

            // Update properties (example)
            existingSchedule.DayOfWeek = updatedSchedule.DayOfWeek;
            existingSchedule.StartTime = updatedSchedule.StartTime;
            existingSchedule.EndTime = updatedSchedule.EndTime;
            existingSchedule.DoctorId = updatedSchedule.DoctorId;

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
        public async Task<ActionResult> Delete(int id)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            _unitOfWork.DoctorSchedules.Remove(schedule);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
