using ClinicManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.DAL.Repositories
{
    /// <summary>
    /// Provides data access operations for <see cref="Patient"/> entities.
    /// Implements the <see cref="IPatientRepository"/> interface using Entity Framework Core.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        /// <summary>
        /// The database context for clinic management.
        /// </summary>
        private readonly ClinicManagementDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used for data access.</param>
        public PatientRepository(ClinicManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all patients that are not marked as deleted.
        /// </summary>
        /// <returns>A list of active <see cref="Patient"/> entities.</returns>
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.Where(p => !p.IsDeleted).ToListAsync();
        }


        /// <summary>
        /// Asynchronously retrieves a patient by their unique identifier, if not deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>
        /// <returns>The <see cref="Patient"/> if found and not deleted; otherwise, null.</returns>
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }


        /// <summary>
        /// Asynchronously adds a new patient to the database.
        /// Sets <see cref="Patient.CreatedAt"/> and <see cref="Patient.IsDeleted"/> flags automatically.
        /// </summary>
        /// <param name="patient">The <see cref="Patient"/> entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>

        public async Task AddAsync(Patient patient)
        {
            patient.CreatedAt = DateTime.UtcNow;
            patient.IsDeleted = false;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Asynchronously updates an existing patient in the database.
        /// </summary>
        /// <param name="patient">The <see cref="Patient"/> entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously performs a soft delete on a patient, marking them as deleted but not removing from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to soft delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SoftDeleteAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (patient == null) return;
            patient.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
