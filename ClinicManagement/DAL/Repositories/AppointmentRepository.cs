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

        /// <inheritdoc/>
        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        /// <inheritdoc/>
        public async Task<Appointment> GetByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }

        /// <inheritdoc/>
        public void Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }
    }
}
