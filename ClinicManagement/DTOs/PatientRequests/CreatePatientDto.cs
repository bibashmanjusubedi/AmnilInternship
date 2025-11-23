using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.PatientRequests
{
    /// <summary>
    /// Data Transfer Object for creating a new patient.
    /// Contains personal and contact information required for patient registration.
    /// Property descriptions are inherited from the <see cref="ClinicManagement.Models.Patient"/> model.
    /// </summary>
    public class CreatePatientDto
    {
        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.FirstName"/> for details.
        /// </summary>
        [Required,MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Patient.LastName"/>.
        /// </summary>
        [Required,MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Patient.Email"/>.
        /// </summary>
        [EmailAddress,MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Patient.PhoneNumber"/>.
        /// </summary>
        [Phone,MaxLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Patient.DateOfBirth"/>.
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

    }
}
