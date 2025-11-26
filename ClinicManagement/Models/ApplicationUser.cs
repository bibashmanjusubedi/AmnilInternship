using Microsoft.AspNetCore.Identity;

namespace ClinicManagement.Models
{
    /// <summary>
    /// Extends ASP.NET IdentityUser with clinic-specific fields.
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        /// <summary>
        /// The full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// When the user account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
