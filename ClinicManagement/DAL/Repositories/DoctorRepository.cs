using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.DAL.Repositories
{
    /// <summary>
    /// Repository implementation for managing Doctor entities.
    /// Provides asynchronous CRUD operations using EF Core DbContext.
    /// </summary>
    public class DoctorRepository: IDoctorRepository
    {
        /// <summary>
        /// The EF Core database context for accessing the Clinic Management data store.
        /// This field is initialized once via constructor injection and is used by the repository to perform data operations.
        /// </summary>
        private readonly ClinicManagementDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for accessing Doctor entities.</param>
        public DoctorRepository(ClinicManagementDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id);
        }

    }
}
