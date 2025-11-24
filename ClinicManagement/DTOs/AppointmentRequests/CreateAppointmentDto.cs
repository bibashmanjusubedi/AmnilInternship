using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.DTOs.AppointmentRequests
{
    /// <summary>
    /// Data Transfer Object for Creating Appoinment
    /// </summary>
    public class CreateAppointmentDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.PatientId"/> for details.
        /// </summary>
        public int PatientId { get; set; }
        
        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.DoctorId"/> for details.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.AppointmentDate"/> for details.
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.Description"/> for details.
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
