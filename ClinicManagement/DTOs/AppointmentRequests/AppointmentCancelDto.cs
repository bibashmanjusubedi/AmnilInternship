using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.AppointmentRequests
{
    /// <summary>
    /// Data Transfer Object for cancelling an appointment
    /// </summary>
    public class AppointmentCancelDto
    {
        [Required]
        [MaxLength(500)]
        public string CancellationReason { get; set; }
    }
}
