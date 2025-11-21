using ClinicManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManagement.DAL.Repositories
{
    /// <summary>
    /// Defines the contract for patient data access operations.
    /// Provides methods for retrieving, creating, updating, and soft deleting patient records.
    /// </summary>
    public interface IPatientRepository
    {
        /// <summary>
        /// Asynchronously retrieves all patients that are not marked as deleted.
        /// </summary>
        /// <returns>A list of active <see cref="Patient"/> entities.</returns>
        Task<IEnumerable<Patient>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a patient by their unique identifier, if not deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>
        /// <returns>The <see cref="Patient"/> if found and not deleted; otherwise, null.</returns>
        Task<Patient> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously adds a new patient to the database.
        /// </summary>
        /// <param name="patient">The <see cref="Patient"/> entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Patient patient);

        /// <summary>
        /// Asynchronously updates an existing patient in the database.
        /// </summary>
        /// <param name="patient">The <see cref="Patient"/> entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Patient patient);

        /// <summary>
        /// Asynchronously performs a soft delete on a patient, marking them as deleted but not removing from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to soft delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SoftDeleteAsync(int id);
    }
}
