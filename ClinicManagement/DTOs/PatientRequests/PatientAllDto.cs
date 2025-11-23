namespace ClinicManagement.DTOs.PatientRequests
{
    /// <summary>
    /// DTO for presenting basic patient information to API consumers.
    /// </summary>
    public class PatientAllDto
    {
        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.Id"/> for details.
        /// </summary>
        public int Id { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.FirstName"/> for details.
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.LastName"/> for details.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.Email"/> for details.
        /// </summary>
        public string Email { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.PhoneNumber"/> for details.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>.  
        /// See <see cref="ClinicManagement.Models.Patient.DateOfBirth"/> for details.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
