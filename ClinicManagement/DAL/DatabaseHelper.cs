using ClinicManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClinicManagement.DAL
{
    /// <summary>
    /// Represents the database context for the Clinic Management system.
    /// Manages the entity sets for patients, appointments, doctors, and doctor schedules, and supports ASP.NET Identity.
    /// </summary>
    public class ClinicManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicManagementDbContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public ClinicManagementDbContext(DbContextOptions<ClinicManagementDbContext> options): base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Patient}"/> representing patients in the database.
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Appointment}"/> representing appointments in the database.
        /// </summary
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Doctor}"/> representing doctors in the database.
        /// </summary>
        public DbSet<Doctor> Doctors { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{DoctorSchedule}"/> representing doctor schedules in the database.
        /// </summary>
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    }
}
