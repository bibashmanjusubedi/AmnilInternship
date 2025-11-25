using ClinicManagement.Models;

namespace ClinicManagement.DTOs.AppointmentRequests
{
    /// <summary>
    /// Data Transfer Object for detailed information about an appointment
    /// </summary>
    public class AppointmentDetailDto
    {

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.Id"/> for details.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.PatientId"/> for details.
        /// </summary>
        public int PatientId { get; set; }
        
        /// <summary>
        /// Patient Name which is patient first and last name combined 
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.DoctorId"/> for details.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Doctor Name which is doctor full name 
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.AppointmentDate"/> for details.
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.Description"/> for details.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.Status"/> for details.
        /// </summary>
        public AppointmentStatus Status { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.Models.Appointment.CreatedAt"/> for details.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
