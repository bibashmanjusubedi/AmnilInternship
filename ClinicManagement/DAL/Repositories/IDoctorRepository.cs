using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.Models;


namespace ClinicManagement.DAL.Repositories
{
    /// <summary>
    /// Defines the repository interface for managing Doctor entities.
    /// Provides asynchronous CRUD operations for doctors in the clinic management system.
    /// </summary>
    public interface IDoctorRepository
    {
        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a collection of all <see cref="Doctor"/> entities.</returns>
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        /// <summary>
        /// Retrieves a doctor by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the <see cref="Doctor"/> entity if found; otherwise, null.</returns>
        Task<Doctor> GetDoctorByIdAsync(int id);

        /// <summary>
        /// Adds a new doctor asynchronously to the data store.
        /// </summary>
        /// <param name="doctor">The <see cref="Doctor"/> entity to add.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task AddDoctorAsync(Doctor doctor);

        /// <summary>
        /// Updates an existing doctor asynchronously in the data store.
        /// </summary>
        /// <param name="doctor">The <see cref="Doctor"/> entity with updated information.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateDoctorAsync(Doctor doctor);

        /// <summary>
        /// Deletes a doctor asynchronously by its unique identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteDoctorAsync(int id);
    }
}
