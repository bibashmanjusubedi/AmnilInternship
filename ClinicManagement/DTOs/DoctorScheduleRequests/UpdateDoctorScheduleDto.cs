using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.DoctorScheduleRequests
{
    /// <summary>
    /// DTO for updating a doctor's weekly schedule.
    /// </summary>
    public class UpdateDoctorScheduleDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.DoctorId"/> for details.
        /// </summary>
        [Required]
        public int DoctorId { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.DayOfWeek"/> for details.
        /// </summary>
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.StartTime"/> for details.
        /// </summary>
        [Required]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.EndTime"/> for details.
        /// </summary>
        [Required]
        public TimeSpan EndTime { get; set; }
      
    }
}
