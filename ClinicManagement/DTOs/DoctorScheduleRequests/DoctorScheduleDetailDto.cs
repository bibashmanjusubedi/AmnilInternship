namespace ClinicManagement.DTOs.DoctorScheduleRequests
{
    /// <summary>
    /// Gets a specific doctor schedule by its ID.
    /// </summary>
    /// <param name="id">The ID of the doctor schedule.</param>
    /// <returns>The doctor schedule matching the provided ID.</returns>
    public class DoctorScheduleDetailDto
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
        public string DoctorName { get; set; } // Optional doctor info

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
