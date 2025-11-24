namespace ClinicManagement.DTOs.DoctorScheduleRequests
{
    /// <summary>
    /// Data Transfer Object for retrieving a doctor's schedule by doctor ID.
    /// </summary>
    public class DoctorScheduleByDoctorDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.Id"/> for details.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.DoctorId"/> for details.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.DayOfWeek"/> for details.
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.StartTime"/> for details.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.DoctorSchedule.EndTime"/> for details.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Doctor name for the schedule for easier identification.
        /// </summary>
        public string DoctorName { get; set; }
    }
}
