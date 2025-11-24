namespace ClinicManagement.DTOs.DoctorScheduleRequests
{
    /// <summary>
    /// DTO for creating a doctor's weekly schedule.
    /// </summary>
    public class CreateDoctorScheduleDto
    {
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
    }
}
