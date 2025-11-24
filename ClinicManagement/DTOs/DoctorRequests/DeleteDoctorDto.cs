namespace ClinicManagement.DTOs.DoctorRequests
{
    /// <summary>
    /// DTO to represent a request to delete a doctor by ID.
    /// </summary>
    public class DeleteDoctorDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.Models.Doctor.Id"/> for details.
        /// </summary>
        public int Id { get; set; }
    }
}
