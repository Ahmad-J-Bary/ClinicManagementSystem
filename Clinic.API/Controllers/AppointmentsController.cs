using Microsoft.AspNetCore.Mvc;
using Clinic.Application.DTOs.Appointment;
using Clinic.Domain.Enums;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// Controller for managing appointments in the clinic management system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(ILogger<AppointmentsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all appointments
        /// </summary>
        /// <returns>List of appointments</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments()
        {
            _logger.LogInformation("Getting all appointments");
            
            // Mock data for demonstration
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Id = 1,
                    StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(11),
                    Reason = "Regular checkup",
                    Status = AppointmentStatus.Confirmed,
                    Notes = "Patient requested morning appointment",
                    IsEmergency = false,
                    PatientId = 1,
                    PatientName = "John Doe",
                    DoctorId = 1,
                    DoctorName = "Dr. Sarah Johnson",
                    DoctorSpecialization = "Cardiology",
                    DateCreated = DateTime.UtcNow.AddDays(-2)
                },
                new AppointmentDto
                {
                    Id = 2,
                    StartTime = DateTime.UtcNow.AddDays(2).AddHours(14),
                    EndTime = DateTime.UtcNow.AddDays(2).AddHours(15),
                    Reason = "Follow-up consultation",
                    Status = AppointmentStatus.Pending,
                    Notes = "Patient needs test results review",
                    IsEmergency = false,
                    PatientId = 2,
                    PatientName = "Jane Smith",
                    DoctorId = 2,
                    DoctorName = "Dr. Michael Brown",
                    DoctorSpecialization = "Pediatrics",
                    DateCreated = DateTime.UtcNow.AddDays(-1)
                }
            };

            return Ok(appointments);
        }

        /// <summary>
        /// Get a specific appointment by ID
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Appointment details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            _logger.LogInformation("Getting appointment with ID: {AppointmentId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }

            // Mock data for demonstration
            var appointment = new AppointmentDto
            {
                Id = id,
                StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                EndTime = DateTime.UtcNow.AddDays(1).AddHours(11),
                Reason = "Regular checkup",
                Status = AppointmentStatus.Confirmed,
                Notes = "Patient requested morning appointment",
                IsEmergency = false,
                PatientId = 1,
                PatientName = "John Doe",
                DoctorId = 1,
                DoctorName = "Dr. Sarah Johnson",
                DoctorSpecialization = "Cardiology",
                DateCreated = DateTime.UtcNow.AddDays(-2)
            };

            return Ok(appointment);
        }

        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <param name="createAppointmentDto">Appointment creation data</param>
        /// <returns>Created appointment</returns>
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
        {
            _logger.LogInformation("Creating new appointment for patient {PatientId} with doctor {DoctorId}", 
                createAppointmentDto.PatientId, createAppointmentDto.DoctorId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createAppointmentDto.StartTime >= createAppointmentDto.EndTime)
            {
                return BadRequest("Start time must be before end time");
            }

            if (createAppointmentDto.StartTime <= DateTime.UtcNow)
            {
                return BadRequest("Appointment cannot be scheduled in the past");
            }

            // Mock creation for demonstration
            var appointment = new AppointmentDto
            {
                Id = new Random().Next(1000, 9999),
                StartTime = createAppointmentDto.StartTime,
                EndTime = createAppointmentDto.EndTime,
                Reason = createAppointmentDto.Reason,
                Status = AppointmentStatus.Pending,
                Notes = createAppointmentDto.Notes,
                IsEmergency = createAppointmentDto.IsEmergency,
                PatientId = createAppointmentDto.PatientId,
                PatientName = "John Doe", // This would come from the database
                DoctorId = createAppointmentDto.DoctorId,
                DoctorName = "Dr. Sarah Johnson", // This would come from the database
                DoctorSpecialization = "Cardiology", // This would come from the database
                DateCreated = DateTime.UtcNow
            };

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Get appointments by patient ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>List of patient appointments</returns>
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByPatient(int patientId)
        {
            _logger.LogInformation("Getting appointments for patient ID: {PatientId}", patientId);

            if (patientId <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            // Mock data for demonstration
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Id = 1,
                    StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(11),
                    Reason = "Regular checkup",
                    Status = AppointmentStatus.Confirmed,
                    Notes = "Patient requested morning appointment",
                    IsEmergency = false,
                    PatientId = patientId,
                    PatientName = "John Doe",
                    DoctorId = 1,
                    DoctorName = "Dr. Sarah Johnson",
                    DoctorSpecialization = "Cardiology",
                    DateCreated = DateTime.UtcNow.AddDays(-2)
                }
            };

            return Ok(appointments);
        }

        /// <summary>
        /// Get appointments by doctor ID
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <returns>List of doctor appointments</returns>
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctor(int doctorId)
        {
            _logger.LogInformation("Getting appointments for doctor ID: {DoctorId}", doctorId);

            if (doctorId <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            // Mock data for demonstration
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Id = 1,
                    StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(11),
                    Reason = "Regular checkup",
                    Status = AppointmentStatus.Confirmed,
                    Notes = "Patient requested morning appointment",
                    IsEmergency = false,
                    PatientId = 1,
                    PatientName = "John Doe",
                    DoctorId = doctorId,
                    DoctorName = "Dr. Sarah Johnson",
                    DoctorSpecialization = "Cardiology",
                    DateCreated = DateTime.UtcNow.AddDays(-2)
                }
            };

            return Ok(appointments);
        }

        /// <summary>
        /// Confirm an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Updated appointment</returns>
        [HttpPut("{id}/confirm")]
        public async Task<ActionResult<AppointmentDto>> ConfirmAppointment(int id)
        {
            _logger.LogInformation("Confirming appointment with ID: {AppointmentId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }

            // Mock confirmation for demonstration
            var appointment = new AppointmentDto
            {
                Id = id,
                StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                EndTime = DateTime.UtcNow.AddDays(1).AddHours(11),
                Reason = "Regular checkup",
                Status = AppointmentStatus.Confirmed,
                Notes = "Patient requested morning appointment",
                ConfirmedAt = DateTime.UtcNow,
                IsEmergency = false,
                PatientId = 1,
                PatientName = "John Doe",
                DoctorId = 1,
                DoctorName = "Dr. Sarah Johnson",
                DoctorSpecialization = "Cardiology",
                DateCreated = DateTime.UtcNow.AddDays(-2)
            };

            return Ok(appointment);
        }

        /// <summary>
        /// Cancel an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="reason">Cancellation reason</param>
        /// <returns>Updated appointment</returns>
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<AppointmentDto>> CancelAppointment(int id, [FromBody] string reason)
        {
            _logger.LogInformation("Cancelling appointment with ID: {AppointmentId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }

            if (string.IsNullOrWhiteSpace(reason))
            {
                return BadRequest("Cancellation reason is required");
            }

            // Mock cancellation for demonstration
            var appointment = new AppointmentDto
            {
                Id = id,
                StartTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                EndTime = DateTime.UtcNow.AddDays(1).AddHours(11),
                Reason = "Regular checkup",
                Status = AppointmentStatus.Cancelled,
                Notes = "Patient requested morning appointment",
                CancellationReason = reason,
                IsEmergency = false,
                PatientId = 1,
                PatientName = "John Doe",
                DoctorId = 1,
                DoctorName = "Dr. Sarah Johnson",
                DoctorSpecialization = "Cardiology",
                DateCreated = DateTime.UtcNow.AddDays(-2)
            };

            return Ok(appointment);
        }
    }
}

