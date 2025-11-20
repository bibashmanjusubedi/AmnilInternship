using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Models
{
    /// <summary>
    /// Represents a doctor's availability schedule on a specific day of the week and time range.
    /// </summary>
    public class DoctorSchedule
    {
        /// <summary>
        /// Gets or sets the unique identifier for the doctor's schedule entry.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the associated doctor.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the day of the week for this schedule.
        /// This is a required field.
        /// </summary>
        [Required]
        public DayOfWeek DayOfWeek { get; set; }


        /// <summary>
        /// Gets or sets the start time of the doctor's availability for the day.
        /// </summary>

        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the doctor's availability for the day.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        // Navigation property: Many to one relationship with Doctor
        /// <summary>
        /// Navigation property to the doctor associated with this schedule.
        /// </summary>
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }
    }
}
