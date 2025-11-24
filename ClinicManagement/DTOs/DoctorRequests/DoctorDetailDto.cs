namespace ClinicManagement.DTOs.DoctorRequests
{
    /// <summary>
    /// DTO representing detailed information about a doctor.
    /// </summary>
    public class DoctorDetailDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Id"/> for details.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.FullName"/> for details.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Specialization"/> for details.
        /// </summary>
        public string Specialization { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Email"/> for details.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Phone"/> for details.
        /// </summary>
        public string Phone { get; set; }
    }
}
