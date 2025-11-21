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
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified database context and patient repository.
        /// </summary>
        /// <param name="context">The database context to coordinate database transactions.</param>
        /// <param name="patientRepository">The patient repository instance to use for patient data operations.</param>

        public UnitOfWork(ClinicManagementDbContext context, IPatientRepository patientRepository)
        {
            _context = context;
            Patients = patientRepository;
        }

        /// <summary>
        /// Asynchronously saves all changes made in repositories to the database in a single transaction.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
