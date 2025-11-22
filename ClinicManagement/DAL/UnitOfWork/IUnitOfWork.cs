using System.Threading.Tasks;
using ClinicManagement.DAL.Repositories;

namespace ClinicManagement.DAL.UnitOfWork
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern in the Clinic Management system.
    /// Provides access to repositories and coordinates saving changes as a single transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the patient repository for managing patient-related data operations.
        /// </summary>
        IPatientRepository Patients { get; }

        /// <summary>
        /// Gets the appointment repository for managing appointment-related data operations.
        /// </summary>
        IAppointmentRepository Appointments { get; }

        /// <summary>
        /// Gets the doctor repository for managing doctor-related data operations.
        /// </summary>
        IDoctorRepository Doctors { get; }

        /// <summary>
        /// Gets the doctor schedule repository for managing doctor schedule-related data operations.
        /// </summary>
        IDoctorScheduleRepository DoctorSchedules { get; }

        /// <summary>
        /// Asynchronously saves all pending changes made in the context to the database.
        /// Commits all transactional changes across repositories.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();
    }
}