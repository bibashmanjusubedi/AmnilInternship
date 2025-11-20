using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Models
{
    /// <summary>
    /// Represents a doctor in the clinic management system.
    /// Contains personal details and related appointments and schedules.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Gets or sets the unique identifier for the doctor.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the doctor.
        /// This field is required and has a maximum length of 150 characters.
        /// </summary>
        [Required, MaxLength(150)]
        public string FullName { get; set; }


        /// <summary>
        /// Gets or sets the specialization of the doctor.
        /// Maximum length of 150 characters.
        /// </summary>
        [MaxLength(150)]
        public string Specialization { get; set; }

        /// <summary>
        /// Gets or sets the email address of the doctor.
        /// Validated as an email and has a maximum length of 255 characters.
        /// </summary>
        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the doctor.
        /// Validated as a phone number with maximum length of 20 characters.
        /// </summary>
        [Phone, MaxLength(20)]
        public string Phone { get; set; }

        // Navigation properties
        // One-To-Many relationship with Appointment
        /// <summary>
        /// Navigation property for the appointments associated with the doctor.
        /// Represents a collection of appointments handled by this doctor.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; }

        // One to Many relationship with DoctorSchedule
        /// <summary>
        /// Navigation property for the doctor's weekly availability schedules.
        /// Represents a collection of schedule entries defining available time slots.
        /// </summary>
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
    }
}
