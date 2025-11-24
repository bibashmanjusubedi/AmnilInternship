using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.DoctorRequests
{
    /// <summary>
    /// Data Transfer Object for Doctor entity for listing doctors.
    /// </summary>
    public class DoctorAllDto
    {

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Id"/> for details.
        /// </summary>
        public int Id { get; set; }
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
