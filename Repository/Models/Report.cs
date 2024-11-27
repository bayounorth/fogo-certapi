namespace FogoCertApi.Repository.Models
{
    public class Report :BaseModel
    {
        public DateTime ReportDate { get; set; }
        public int VesselId { get; set; }
        public int TotalForms { get; set; }
        public int TotalFormsCompleted { get; set; }
        public int ComplianceId { get; set; }
        public string VesselName { get; set; }
        public string Name { get; set; }
        public List<ReportFormData> Forms { get; set; }

    }
}
