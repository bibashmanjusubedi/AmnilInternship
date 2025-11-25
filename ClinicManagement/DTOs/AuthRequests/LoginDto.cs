namespace ClinicManagement.DTOs.AuthRequests
{
    /// <summary>
    /// Data Transfer Object for user login.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// See <see cref="ClinicManagement.DTOs.AuthRequests.RegisterDto.Email"/>.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// See <see cref="ClinicManagement.DTOs.AuthRequests.RegisterDto.Password"/>.
        /// </summary>
        public string Password { get; set; }
    }
}
