using Microsoft.AspNetCore.Mvc;
using Clinic.Application.DTOs.Patient;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// Controller for managing patients in the clinic management system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(ILogger<PatientsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns>List of patients</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            _logger.LogInformation("Getting all patients");
            
            // Mock data for demonstration
            var patients = new List<PatientDto>
            {
                new PatientDto
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@email.com",
                    PhoneNumber = "+1234567890",
                    Address = "123 Main St, City, State",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    PatientIdNumber = "P001",
                    InsuranceProvider = "Health Insurance Co.",
                    BloodType = "O+",
                    IsActive = true,
                    DateCreated = DateTime.UtcNow.AddDays(-30)
                },
                new PatientDto
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@email.com",
                    PhoneNumber = "+1234567891",
                    Address = "456 Oak Ave, City, State",
                    DateOfBirth = new DateTime(1985, 8, 22),
                    PatientIdNumber = "P002",
                    InsuranceProvider = "Medical Care Inc.",
                    BloodType = "A+",
                    IsActive = true,
                    DateCreated = DateTime.UtcNow.AddDays(-15)
                }
            };

            return Ok(patients);
        }

        /// <summary>
        /// Get a specific patient by ID
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Patient details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
            _logger.LogInformation("Getting patient with ID: {PatientId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            // Mock data for demonstration
            var patient = new PatientDto
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@email.com",
                PhoneNumber = "+1234567890",
                Address = "123 Main St, City, State",
                DateOfBirth = new DateTime(1990, 5, 15),
                PatientIdNumber = "P001",
                InsuranceProvider = "Health Insurance Co.",
                BloodType = "O+",
                IsActive = true,
                DateCreated = DateTime.UtcNow.AddDays(-30)
            };

            return Ok(patient);
        }

        /// <summary>
        /// Create a new patient
        /// </summary>
        /// <param name="createPatientDto">Patient creation data</param>
        /// <returns>Created patient</returns>
        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient([FromBody] CreatePatientDto createPatientDto)
        {
            _logger.LogInformation("Creating new patient: {Email}", createPatientDto.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mock creation for demonstration
            var patient = new PatientDto
            {
                Id = new Random().Next(1000, 9999),
                FirstName = createPatientDto.FirstName,
                LastName = createPatientDto.LastName,
                Email = createPatientDto.Email,
                PhoneNumber = createPatientDto.PhoneNumber,
                Address = createPatientDto.Address,
                DateOfBirth = createPatientDto.DateOfBirth,
                PatientIdNumber = createPatientDto.PatientIdNumber,
                InsuranceProvider = createPatientDto.InsuranceProvider,
                InsurancePolicyNumber = createPatientDto.InsurancePolicyNumber,
                EmergencyContactName = createPatientDto.EmergencyContactName,
                EmergencyContactPhone = createPatientDto.EmergencyContactPhone,
                BloodType = createPatientDto.BloodType,
                Allergies = createPatientDto.Allergies,
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Update an existing patient
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <param name="updatePatientDto">Updated patient data</param>
        /// <returns>Updated patient</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> UpdatePatient(int id, [FromBody] CreatePatientDto updatePatientDto)
        {
            _logger.LogInformation("Updating patient with ID: {PatientId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mock update for demonstration
            var patient = new PatientDto
            {
                Id = id,
                FirstName = updatePatientDto.FirstName,
                LastName = updatePatientDto.LastName,
                Email = updatePatientDto.Email,
                PhoneNumber = updatePatientDto.PhoneNumber,
                Address = updatePatientDto.Address,
                DateOfBirth = updatePatientDto.DateOfBirth,
                PatientIdNumber = updatePatientDto.PatientIdNumber,
                InsuranceProvider = updatePatientDto.InsuranceProvider,
                InsurancePolicyNumber = updatePatientDto.InsurancePolicyNumber,
                EmergencyContactName = updatePatientDto.EmergencyContactName,
                EmergencyContactPhone = updatePatientDto.EmergencyContactPhone,
                BloodType = updatePatientDto.BloodType,
                Allergies = updatePatientDto.Allergies,
                IsActive = true,
                DateCreated = DateTime.UtcNow.AddDays(-30)
            };

            return Ok(patient);
        }

        /// <summary>
        /// Delete a patient
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            _logger.LogInformation("Deleting patient with ID: {PatientId}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid patient ID");
            }

            // Mock deletion for demonstration
            return NoContent();
        }
    }
}

