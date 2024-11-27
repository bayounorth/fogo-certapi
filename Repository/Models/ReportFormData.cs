namespace FogoCertApi.Repository.Models
{
    public class ReportFormData : Form
    {
        public int Status { get; set; }
        public string FormStatus { get; set; }
        public FormDataEntry FormDataEntry { get; set; }

    }
}
