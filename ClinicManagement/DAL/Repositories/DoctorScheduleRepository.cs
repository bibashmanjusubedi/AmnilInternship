using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Models;

namespace ClinicManagement.DAL.Repositories
{
    /// <summary>
    /// Repository implementation for managing <see cref="DoctorSchedule"/> entities.
    /// Provides asynchronous CRUD operations and querying by doctor ID using Entity Framework Core.
    /// </summary>
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly ClinicManagementDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorScheduleRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public DoctorScheduleRepository(ClinicManagementDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<DoctorSchedule> GetByIdAsync(int id)
        {
            return await _context.DoctorSchedules.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DoctorSchedule>> GetAllAsync()
        {
            return await _context.DoctorSchedules.Include(ds => ds.Doctor).ToListAsync();
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorSchedules
                .Where(ds => ds.DoctorId == doctorId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(DoctorSchedule schedule)
        {
            await _context.DoctorSchedules.AddAsync(schedule);
        }

        /// <inheritdoc/>
        public void Update(DoctorSchedule schedule)
        {
            _context.DoctorSchedules.Update(schedule);
        }
        
        /// <inheritdoc/>
        public void Remove(DoctorSchedule schedule)
        {
            _context.DoctorSchedules.Remove(schedule);
        }
    }
}
