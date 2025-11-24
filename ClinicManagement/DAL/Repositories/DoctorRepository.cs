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
        /// <summary>
        /// Retrieves all doctors asynchronously from the database.
        /// </summary>
        /// <returns>A task representing the operation.
        /// The task result contains IEnumerable collection of all Doctor entities.</returns>
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific doctor by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor entity.</param>
        /// <returns>A task representing the operation.
        /// The task result contains the Doctor entity if found; otherwise, null.</returns>
        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        /// <summary>
        /// Adds a new doctor entity asynchronously to the database.
        /// </summary>
        /// <param name="doctor">The Doctor entity to add.</param>
        /// <returns>A task representing the asynchronous add operation.</returns>
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing doctor entity asynchronously in the database.
        /// </summary>
        /// <param name="doctor">The Doctor entity with updated values.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a doctor entity asynchronously from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Asynchronously checks if a doctor with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to check.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains <c>true</c> if a doctor with the given ID exists; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id);
        }

    }
}
