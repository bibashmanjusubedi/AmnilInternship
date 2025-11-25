using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Models;
using ClinicManagement.DAL.UnitOfWork;
using System;
using System.Threading.Tasks;
using ClinicManagement.DTOs.AppointmentRequests;
using System.Linq;

namespace ClinicManagement.Controllers
{
    /// <summary>
    /// API Controller for managing appointment operations in the Clinic Management system.
    /// Supports appointment creation, rescheduling, cancellation, viewing by doctor/admin, and completion status updates.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        /// <summary>
        /// Unit of Work instance for accessing appointment and related repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class
        /// with the specified Unit of Work.
        /// </summary>
        /// <param name="unitOfWork">The Unit of Work for managing data operations.</param>
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Receptionist/Admin: Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment details submitted in the request body.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> with the created appointment, or error response if creation fails.
        /// </returns>
        // Receptionist/Admin: Create a new appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                Status = AppointmentStatus.Scheduled
            };
            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Receptionist: Reschedules an appointment by updating its date and time.
        /// </summary>
        /// <param name="id">The ID of the appointment to reschedule.</param>
        /// <param name="newDate">The new appointment date and time.</param>
        /// <returns>
        /// <see cref="NoContentResult"/> if successful, error or not found responses otherwise.
        /// </returns>
        // Receptionist: Reschedule an appointment
        [HttpPut("{id}/reschedule")]
        public async Task<IActionResult> RescheduleAppointment(int id, [FromBody] RescheduleAppointmentDto dto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NotFound();
            if (appointment.Status != AppointmentStatus.Scheduled)
                return BadRequest("Cannot reschedule a completed or cancelled appointment.");

            appointment.AppointmentDate = dto.NewDate;
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Receptionist: Cancels a scheduled appointment by updating its status.
        /// </summary>
        /// <param name="id">The ID of the appointment to cancel.</param>
        /// <returns>
        /// <see cref="NoContentResult"/> if successful, error or not found responses otherwise.
        /// </returns>
        // Receptionist: Cancel an appointment
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NotFound();
            if (appointment.Status != AppointmentStatus.Scheduled)
                return BadRequest("Only scheduled appointments can be cancelled.");

            appointment.Status = AppointmentStatus.Cancelled;
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Doctor: Retrieves all appointments assigned to a specific doctor.
        /// </summary>
        /// <param name="doctorId">The ID of the doctor whose appointments to retrieve.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> containing a collection of appointment entities.
        /// </returns>
        // Doctor: View own appointments
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId)
        {
            var appointments = await _unitOfWork.Appointments.GetByDoctorIdAsync(doctorId);

            var appointmentDtos = appointments.Select(a => new AppointmentByDoctorDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = a.Patient != null ? $"{a.Patient.FirstName} {a.Patient.LastName}" : "",
                AppointmentDate = a.AppointmentDate,
                Description = a.Description,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Ok(appointmentDtos);
        }

        /// <summary>
        /// Doctor: Marks an appointment as completed by updating its status.
        /// </summary>
        /// <param name="id">The ID of the appointment to complete.</param>
        /// <returns>
        /// <see cref="NoContentResult"/> if successful, error or not found responses otherwise.
        /// </returns>
        // Doctor: Mark appointment as completed
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkAppointmentCompleted(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NotFound();
            if (appointment.Status != AppointmentStatus.Scheduled)
                return BadRequest("Only scheduled appointments can be marked as completed.");

            appointment.Status = AppointmentStatus.Completed;
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Admin: Retrieves all appointments in the system.
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/> containing all appointment entities.
        /// </returns>
        // Admin: View all appointments
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync();

            var appointmentDtos = appointments.Select(a => new AppointmentAllDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = a.Patient != null ? $"{a.Patient.FirstName} {a.Patient.LastName}" : "",
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.FullName,
                AppointmentDate = a.AppointmentDate,
                Description = a.Description,
                Status = a.Status,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Ok(appointmentDtos);
        }


        /// <summary>
        /// Retrieves details for a specific appointment by ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> containing the appointment if found, or error responses otherwise.
        /// </returns>
        // Get appointment by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var dto = new AppointmentDetailDto
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient != null ? $"{appointment.Patient.FirstName} {appointment.Patient.LastName}" : "",
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor?.FullName,
                AppointmentDate = appointment.AppointmentDate,
                Description = appointment.Description,
                Status = appointment.Status,
                CreatedAt = appointment.CreatedAt
            };

            return Ok(dto);
        }
    }
}
