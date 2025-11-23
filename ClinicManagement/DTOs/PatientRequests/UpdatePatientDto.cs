using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.PatientRequests
{
    /// <summary>
    /// DTO for updating basic patient information.
    /// </summary>
    public class UpdatePatientDto
    {
        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.FirstName"/> for details.
        /// </summary>
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.LastName"/> for details.
        /// </summary>
        [Required, MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.Email"/> for details.
        /// </summary>
        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.PhoneNumber"/> for details.
        /// </summary>
        [Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.DateOfBirth"/> for details.
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
