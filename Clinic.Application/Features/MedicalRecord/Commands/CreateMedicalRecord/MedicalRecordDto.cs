
namespace Clinic.Application.Features.MedicalRecord.Commands.CreateMedicalRecord
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string? Notes { get; set; }
        public int? AppointmentId { get; set; }
    }
}

