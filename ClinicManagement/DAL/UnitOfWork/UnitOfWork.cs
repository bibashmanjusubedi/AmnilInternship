using System.Threading.Tasks;
using ClinicManagement.DAL.Repositories;

namespace ClinicManagement.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Implements the Unit of Work pattern for the Clinic Management system.
        /// Coordinates one or more repository operations and manages saving changes to the database as a single transaction.
        /// </summary>
        private readonly ClinicManagementDbContext _context;


        /// <summary>
        /// Gets the patient repository for handling patient entity operations.
        /// </summary>
        public IPatientRepository Patients { get; }

        /// <summary>
        /// Gets the appointment repository for handling appointment entity operations.
        /// </summary>
        public IAppointmentRepository Appointments { get; }

        /// <summary>
        /// Gets the doctor repository for handling doctor entity operations.
        /// </summary>
        public IDoctorRepository Doctors { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified database context and repositories.
        /// </summary>
        /// <param name="context">The database context to coordinate database transactions.</param>
        /// <param name="patientRepository">The patient repository instance for patient data operations.</param>
        /// <param name="appointmentRepository">The appointment repository instance for appointment data operations.</param>
        /// <param name="doctorRepository">The doctor repository instance for doctor data operations.</param>
        public UnitOfWork(ClinicManagementDbContext context, IPatientRepository patientRepository,IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
        {
            _context = context;
            Patients = patientRepository;
            Appointments = appointmentRepository;
            Doctors = doctorRepository;
        }

        /// <summary>
        /// Asynchronously saves all changes made in repositories to the database as a single transaction.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous save operation. 
        /// The task result contains the number of state entries written to the database.
        /// </returns>
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
