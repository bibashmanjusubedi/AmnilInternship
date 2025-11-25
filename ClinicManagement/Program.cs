using ClinicManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using ClinicManagement.DAL.Repositories;
using ClinicManagement.DAL.UnitOfWork;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Logger(lc => lc  // Logger for soft delete patient deletions
        .Filter.ByIncludingOnly(evt => evt.Properties.ContainsKey("LogType") && evt.Properties["LogType"].ToString().Contains("Deletion"))
        .WriteTo.File("logs/softDeletePatients.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(lc => lc  // Logger for cancellations
        .Filter.ByIncludingOnly(evt => evt.Properties.ContainsKey("LogType") && evt.Properties["LogType"].ToString().Contains("Cancellation"))
        .WriteTo.File("logs/appointmentCancellations.txt", rollingInterval: RollingInterval.Day))
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog(); // for Serilog styleed console uncomment it...for default console dont use it 


builder.Services.AddDbContext<ClinicManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register your repository and unit of work here
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
