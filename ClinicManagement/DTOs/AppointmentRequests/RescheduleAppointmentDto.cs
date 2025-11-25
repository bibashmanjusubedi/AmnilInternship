using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.AppointmentRequests
{
    /// <summary>
    /// Data Transfer Object for Rescheduling an appointment.
    /// </summary>
    public class RescheduleAppointmentDto
    {
        /// <summary>
        /// New date and time for the appointment.
        /// </summary>
        [Required]
        public DateTime NewDate { get; set; }
    }
}
