namespace ClinicManagement.DTOs.AuthRequests
{
    /// <summary>
    /// Data Transfer Object for assigning a role to a user.
    /// </summary>
    public class AssignRoleDto
    {
        /// <summary>
        /// The unique identifier (Id) of the user to assign the role to.
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// The role to assign to the user. Must be one of: "Admin", "Doctor", or "Receptionist".
        /// </summary>
        public string Role { get; set; }  // "Admin", "Doctor", "Receptionist"
    }
}
