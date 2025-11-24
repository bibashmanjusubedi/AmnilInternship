using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.DoctorRequests
{
    /// <summary>
    /// DTO to represent doctor data when creating a new doctor.
    /// </summary>
    public class CreateDoctorDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.FullName"/> for details.
        /// </summary>
        [Required, MaxLength(150)]
        public string FullName { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Specialization"/> for details.
        /// </summary>
        [MaxLength(150)]
        public string Specialization { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Email"/> for details.
        /// </summary>
        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Phone"/> for details.
        /// </summary>
        [Phone, MaxLength(20)]
        public string Phone { get; set; }
    }
}
