using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClinicManagement.Models
{
    /// <summary>
    /// Represents a patient in the clinic management system
    /// Contains personal and contact details along with related appointments
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Gets or sets the unique identifier for the patient
        /// </summary>
        [Key]
        public int Id{get;set;}

        /// <summary>
        /// Gets or sets the first name of the patient
        /// This field is required and has a maximum length of 100 characters
        /// </summary>
        [Required,MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the patient
        /// This field is required and has a maximum length of 100 characters
        /// </summary>

        [Required,MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the patient
        /// This field must be a valid email format and has a maximum length of 255 characters
        /// </summary>

        [EmailAddress,MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the patient
        /// This field must be a valid phone number format and has a maximum length of 20 characters
        /// </summary>

        [Phone,MaxLength(20)]
        public string PhoneNumber { get;set; }

        /// <summary>
        /// Gets or sets the date of birth of the patient
        /// </summary>
        public DateTime DateOfBirth { get;set; }

        /// <summary>
        /// Gets or sets the date and time when the patient record was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Indicates whether the patient record is marked as deleted(soft delete support)
        /// </summary>
        public bool IsDeleted { get; set; }

        // Navigation property : One-To-Many relationship with Appointment
        /// <summary>
        /// Navigtion property for the appointments related to this patient
        /// Represents a collection of appointments that belong to the patient
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
