using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.Models;
using ClinicManagement.Models;

namespace ClinicManagement.DAL.Repositories
{
    // Interface for DoctorSchedule repository
    /// <summary>
    /// Interface for DoctorSchedule repository that defines methods for asynchronous CRUD operations
    /// and querying doctor availability schedules.
    /// </summary>
    public interface IDoctorScheduleRepository
    {
        /// <summary>
        /// Asynchronously gets the doctor schedule by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor schedule.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DoctorSchedule"/> entity if found.</returns>
        Task<DoctorSchedule> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all doctor schedules.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a collection of all <see cref="DoctorSchedule"/> entities.</returns>
        Task<IEnumerable<DoctorSchedule>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves doctor schedules filtered by a specific doctor ID.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a collection of <see cref="DoctorSchedule"/> entities for the specified doctor.</returns>
        Task<IEnumerable<DoctorSchedule>> GetByDoctorIdAsync(int doctorId);

        /// <summary>
        /// Asynchronously adds a new doctor schedule to the data source.
        /// </summary>
        /// <param name="schedule">The <see cref="DoctorSchedule"/> entity to add.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task AddAsync(DoctorSchedule schedule);

        /// <summary>
        /// Updates an existing doctor schedule in the data source.
        /// </summary>
        /// <param name="schedule">The <see cref="DoctorSchedule"/> entity to update.</param>
        void Update(DoctorSchedule schedule);

        /// <summary>
        /// Removes a doctor schedule from the data source.
        /// </summary>
        /// <param name="schedule">The <see cref="DoctorSchedule"/> entity to remove.</param>
        void Remove(DoctorSchedule schedule);
    }
}
