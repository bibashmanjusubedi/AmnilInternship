namespace ClinicManagement.DTOs.DoctorScheduleRequests
{
    /// <summary>
    /// DTO for displaying a doctor's weekly schedule entry in list endpoints.
    /// </summary>
    public class DoctorScheduleAllDto
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
        /// The display name of the doctor with a given id
        /// </summary>
        public string DoctorName { get; set; } // Optional

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
