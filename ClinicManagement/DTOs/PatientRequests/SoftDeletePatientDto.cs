namespace ClinicManagement.DTOs.PatientRequests
{
    /// <summary>
    /// Data Transfer Object for performing a soft delete operation on a patient.
    /// Includes information about the deletion timestamp.
    /// </summary>
    public class SoftDeletePatientDto
    {
        /// <summary>
        /// The date and time when the patient was marked as deleted.
        /// </summary>
        public DateTime DeletedAt {get; set; }
    }
}
