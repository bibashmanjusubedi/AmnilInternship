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

        /// <inheritdoc/>
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.Where(p => !p.IsDeleted).ToListAsync();
        }


        /// <inheritdoc/>
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }



        /// <inheritdoc/>
        public async Task AddAsync(Patient patient)
        {
            patient.CreatedAt = DateTime.UtcNow;
            patient.IsDeleted = false;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }


        /// <inheritdoc/>
        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task SoftDeleteAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (patient == null) return;
            patient.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
