namespace FogoCertApi.Repository.Models
{
    public class ReportForm : BaseModel
    {
        public int ReportId { get; set; }
        public int FormId { get; set; }
        public int Status { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
