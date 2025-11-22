using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Models;

namespace ClinicManagement.DAL.Repositories
{
    /// <summary>
    /// Provides data access operations for Appointment entities in the Clinic Management system.
    /// Implements <see cref="IAppointmentRepository"/> using Entity Framework Core.
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        /// <summary>
        /// The database context used to interact with the Clinic Management data store.
        /// </summary>
        private readonly ClinicManagementDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentRepository"/> class
        /// with the specified database context.
        /// </summary>
        /// <param name="context">The database context for accessing appointments.</param>
        public AppointmentRepository(ClinicManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously adds a new appointment to the database.
        /// </summary>
        /// <param name="appointment">The appointment entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        /// <summary>
        /// Asynchronously retrieves an appointment by its unique identifier,
        /// including associated Patient and Doctor entities.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains
        /// the <see cref="Appointment"/> entity if found, or null otherwise.
        /// </returns>
        public async Task<Appointment> GetByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }

        /// <summary>
        /// Asynchronously retrieves all appointments for a specific doctor,
        /// including associated Patient entities.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains
        /// a collection of <see cref="Appointment"/> entities for the doctor.
        /// </returns>
        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves all appointments in the system,
        /// including associated Patient and Doctor entities.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains
        /// a collection of all <see cref="Appointment"/> entities.
        /// </returns>
        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        /// <summary>
        /// Updates an existing appointment entity in the database.
        /// The appointment should be tracked by the current context.
        /// </summary>
        /// <param name="appointment">The appointment entity with updated values.</param>
        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }

        /// <summary>
        /// Removes an appointment entity from the database.
        /// The appointment should be tracked by the current context.
        /// </summary>
        /// <param name="appointment">The appointment entity to remove.</param>
        public void Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }
    }
}
