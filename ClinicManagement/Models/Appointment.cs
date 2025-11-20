using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;


namespace ClinicManagement.Models
{
    /// <summary>
    /// Represents an appointment scheduled between a patient and a doctor
    /// Contains appointment details such as date, decription, and status
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the appointment
        /// </summary>
        [Key]
        public int Id { get; set; }

        // Foreign Keys
        
        /// <summary>
        /// Gets or sets the foreign key referencing the patient.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the doctor.
        /// </summary>
        public int DoctorId { get;set; }

        /// <summary>
        /// Gets or sets the date and time of the appointment.
        /// </summary>
        public DateTime AppointmentDate { get;set; }

        /// <summary>
        /// Gets or sets the description or notes about the appointment.
        /// Maximum length: 500 characters.
        /// </summary>
        [MaxLength(500)]
        public string Description { get;set; }

        /// <summary>
        /// Gets or sets the status of the appointment (Scheduled, Completed, Cancelled).
        /// </summary>
        [Required]
        public AppointmentStatus Status { get;set; }

        /// <summary>
        /// Gets or sets the date and time when the appointment record was created.
        /// </summary>
        public DateTime CreatedAt { get;set; }

        // Navigation properties
        // Many to one relationship with Patient
        /// <summary>
        /// Gets or sets the navigation property to the related patient.
        /// Used by Entity Framework to load related patient data.
        /// </summary>
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get;set; }


        // Many to one relationship with Doctor
        /// <summary>
        /// Gets or sets the navigation property to the related doctor.
        /// Used by Entity Framework to load related doctor data.
        /// </summary>
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get;set; }
    }

    /// <summary>
    /// Enumeration of the possible appointment statuses : Scheduled, Completed, Cancelled
    /// </summary>
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }
}
