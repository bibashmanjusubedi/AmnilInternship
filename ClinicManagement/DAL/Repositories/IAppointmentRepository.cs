using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicManagement.Models;

namespace ClinicManagement.DAL.Repositories
{
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Defines data access operations for appointment management in the Clinic Management system.
        /// Provides repository methods for creating, updating, retrieving, and deleting appointments.
        /// </summary>
        // Receptionist/Admin: Create a new appointment
        Task AddAsync(Appointment appointment);


        /// <summary>
        /// Retrieves an appointment by its unique identifier.
        /// Used for rescheduling, cancelling, or viewing details.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the matching <see cref="Appointment"/> entity,
        /// or null if not found.
        /// </returns>
        // Receptionist: Reschedule an appointment (change date/time)
        Task<Appointment> GetByIdAsync(int appointmentId);

        /// <summary>
        /// Updates an existing appointment in the database.
        /// Used for operations such as rescheduling, cancelling (by updating status), or marking an appointment as completed.
        /// </summary>
        /// <param name="appointment">The appointment entity with updated properties.</param>
        void Update(Appointment appointment);

        // Receptionist: Cancel an appointment
        // (handled through Update by setting Status)

        // Doctor: View own appointments
        /// <summary>
        /// Retrieves all appointments assigned to a specific doctor.
        /// Used by Doctor to view their appointments.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of matching <see cref="Appointment"/> entities.
        /// </returns>
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);

        // Doctor/Admin: View all appointments
        /// <summary>
        /// Retrieves all appointments in the system.
        /// Used by Admin or Doctor to view appointments.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of <see cref="Appointment"/> entities.
        /// </returns>
        Task<IEnumerable<Appointment>> GetAllAsync();

        // Additional helper for marking appointment as completed
        // (again handled by updating Status)

        // Optionally, delete appointments if needed
        /// <summary>
        /// Deletes an existing appointment from the database.
        /// Use cautiously; typically only for admin actions or cleanup.
        /// </summary>
        /// <param name="appointment">The appointment entity to delete.</param>
        void Delete(Appointment appointment);

        /// <summary>
        /// Returns a queryable collection of all appointments, including related patient
        /// and doctor data, allowing callers to apply additional filtering, sorting,
        /// and pagination before execution.
        public IQueryable<Appointment> GetAll();
    }

}
