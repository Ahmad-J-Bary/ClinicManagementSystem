namespace Clinic.Application.Features.MedicalRecord.Queries.GetMedicalRecordDetail
{
    public class MedicalRecordDetailDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string? Notes { get; set; }
        public string? Symptoms { get; set; }
        public string? VitalSigns { get; set; }
        public string? LabResults { get; set; }
        public string? ImagingResults { get; set; }
        public string? FollowUpInstructions { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public int? AppointmentId { get; set; }
    }
}

