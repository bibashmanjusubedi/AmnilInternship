namespace ClinicManagement.DTOs.AuthRequests
{
    /// <summary>
    /// Data Transfer Object for registering a new user.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// The email address for the new user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password for the new user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The full name of the new user.
        /// </summary>
        public string FullName { get; set; }
    }
}
