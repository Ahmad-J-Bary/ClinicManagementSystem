using AutoMapper;
using Clinic.Application.Contracts.Persistence;
using Clinic.Application.Exceptions;
using MediatR;

namespace Clinic.Application.Features.Patient.Commands.CreatePatient
{
    /// <summary>
    /// Handler for CreatePatientCommand.
    /// Implements the business logic for creating a new patient.
    /// </summary>
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public CreatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            // Validate that patient ID number is unique
            var existingPatient = await _patientRepository.GetPatientByIdNumberAsync(request.PatientIdNumber);
            if (existingPatient != null)
            {
                throw new BadRequestException($"Patient with ID number {request.PatientIdNumber} already exists.");
            }

            // Validate that email is unique
            var existingPatientByEmail = await _patientRepository.GetPatientByEmailAsync(request.Email);
            if (existingPatientByEmail != null)
            {
                throw new BadRequestException($"Patient with email {request.Email} already exists.");
            }

            // Map command to entity
            var patient = _mapper.Map<Domain.Entities.Patient>(request);

            // Update additional fields if provided
            if (!string.IsNullOrEmpty(request.EmergencyContactName) || !string.IsNullOrEmpty(request.EmergencyContactPhone))
            {
                patient.UpdateEmergencyContact(request.EmergencyContactName, request.EmergencyContactPhone);
            }

            if (!string.IsNullOrEmpty(request.BloodType) || !string.IsNullOrEmpty(request.Allergies))
            {
                patient.UpdateMedicalInfo(request.BloodType, request.Allergies);
            }

            // Save to repository
            var createdPatient = await _patientRepository.CreateAsync(patient);

            return createdPatient.Id;
        }
    }
}

