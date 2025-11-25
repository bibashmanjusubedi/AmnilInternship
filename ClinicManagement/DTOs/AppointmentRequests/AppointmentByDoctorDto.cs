using ClinicManagement.Models;

namespace ClinicManagement.DTOs.AppointmentRequests
{
    public class AppointmentByDoctorDto
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
        public string PatientName { get; set; }  // e.g., Patient's full name

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
