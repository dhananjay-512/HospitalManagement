namespace HospitalManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int? DiseaseId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime AdmittedDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Disease? Diseases { get; set; }
    }
}
