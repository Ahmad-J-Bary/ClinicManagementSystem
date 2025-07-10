using Microsoft.AspNetCore.Mvc;
using Clinic.Application.DTOs.Doctor;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// Controller for managing doctors in the clinic management system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(ILogger<DoctorsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all doctors
        /// </summary>
        /// <returns>List of doctors</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            _logger.LogInformation("Getting all doctors");
            
            // Mock data for demonstration
            var doctors = new List<DoctorDto>
            {
                new DoctorDto
                {
                    Id = 1,
                    FirstName = "Dr. Sarah",
                    LastName = "Johnson",
                    Email = "sarah.johnson@clinic.com",
                    PhoneNumber = "+1234567892",
                    Address = "789 Medical Center Dr, City, State",
                    DateOfBirth = new DateTime(1980, 3, 10),
                    MedicalLicenseNumber = "MD12345",
                    Specialization = "Cardiology",
                    Qualifications = "MD, FACC",
                    ConsultationFee = 200.00m,
                    YearsOfExperience = 15,
                    Biography = "Experienced cardiologist with expertise in interventional cardiology.",
                    DepartmentId = 1,
                    DepartmentName = "Cardiology Department",
                    IsActive = true,
                    DateCreated = DateTime.UtcNow.AddDays(-60)
                },
                new DoctorDto
                {
                    Id = 2,
                    FirstName = "Dr. Michael",
                    LastName = "Brown",
                    Email = "michael.brown@clinic.com",
                    PhoneNumber = "+1234567893",
                    Address = "456 Healthcare Blvd, City, State",
                    DateOfBirth = new DateTime(1975, 7, 25),
                    MedicalLicenseNumber = "MD67890",
                    Specialization = "Pediatrics",
                    Qualifications = "MD, FAAP",
                    ConsultationFee = 150.00m,
                    YearsOfExperience = 20,
                    Biography = "Dedicated pediatrician with a passion for child healthcare.",
                    DepartmentId = 2,
                    DepartmentName = "Pediatrics Department",
                    IsActive = true,
                    DateCreated = DateTime.UtcNow.AddDays(-90)
                }
            };

            return Ok(doctors);
        }

        /// <summary>
        /// Get a specific doctor by ID
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Doctor details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            _logger.LogInformation("Getting doctor with ID: {DoctorId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            // Mock data for demonstration
            var doctor = new DoctorDto
            {
                Id = id,
                FirstName = "Dr. Sarah",
                LastName = "Johnson",
                Email = "sarah.johnson@clinic.com",
                PhoneNumber = "+1234567892",
                Address = "789 Medical Center Dr, City, State",
                DateOfBirth = new DateTime(1980, 3, 10),
                MedicalLicenseNumber = "MD12345",
                Specialization = "Cardiology",
                Qualifications = "MD, FACC",
                ConsultationFee = 200.00m,
                YearsOfExperience = 15,
                Biography = "Experienced cardiologist with expertise in interventional cardiology.",
                DepartmentId = 1,
                DepartmentName = "Cardiology Department",
                IsActive = true,
                DateCreated = DateTime.UtcNow.AddDays(-60)
            };

            return Ok(doctor);
        }

        /// <summary>
        /// Get doctors by specialization
        /// </summary>
        /// <param name="specialization">Medical specialization</param>
        /// <returns>List of doctors with the specified specialization</returns>
        [HttpGet("specialization/{specialization}")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctorsBySpecialization(string specialization)
        {
            _logger.LogInformation("Getting doctors with specialization: {Specialization}", specialization);

            // Mock data for demonstration
            var doctors = new List<DoctorDto>
            {
                new DoctorDto
                {
                    Id = 1,
                    FirstName = "Dr. Sarah",
                    LastName = "Johnson",
                    Email = "sarah.johnson@clinic.com",
                    PhoneNumber = "+1234567892",
                    Address = "789 Medical Center Dr, City, State",
                    DateOfBirth = new DateTime(1980, 3, 10),
                    MedicalLicenseNumber = "MD12345",
                    Specialization = specialization,
                    Qualifications = "MD, FACC",
                    ConsultationFee = 200.00m,
                    YearsOfExperience = 15,
                    Biography = $"Experienced {specialization.ToLower()} specialist.",
                    DepartmentId = 1,
                    DepartmentName = $"{specialization} Department",
                    IsActive = true,
                    DateCreated = DateTime.UtcNow.AddDays(-60)
                }
            };

            return Ok(doctors);
        }

        /// <summary>
        /// Get available doctors for a specific time slot
        /// </summary>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <returns>List of available doctors</returns>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAvailableDoctors([FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            _logger.LogInformation("Getting available doctors from {StartTime} to {EndTime}", startTime, endTime);

            if (startTime >= endTime)
            {
                return BadRequest("Start time must be before end time");
            }

            // Mock data for demonstration
            var doctors = new List<DoctorDto>
            {
                new DoctorDto
                {
                    Id = 1,
                    FirstName = "Dr. Sarah",
                    LastName = "Johnson",
                    Email = "sarah.johnson@clinic.com",
                    PhoneNumber = "+1234567892",
                    Address = "789 Medical Center Dr, City, State",
                    DateOfBirth = new DateTime(1980, 3, 10),
                    MedicalLicenseNumber = "MD12345",
                    Specialization = "Cardiology",
                    Qualifications = "MD, FACC",
                    ConsultationFee = 200.00m,
                    YearsOfExperience = 15,
                    Biography = "Experienced cardiologist with expertise in interventional cardiology.",
                    DepartmentId = 1,
                    DepartmentName = "Cardiology Department",
                    IsActive = true,
                    DateCreated = DateTime.UtcNow.AddDays(-60)
                }
            };

            return Ok(doctors);
        }
    }
}

