namespace FogoCertApi.Repository.Models
{
    public class Form : BaseModel
    {
        public string Name { get; set; }

        public string Cycle { get; set; }
        public string Url { get; set; }
        public byte[] FileData { get; set; }
        public int? FileCategoriesId { get; set; }
        public string FileOrFormPage { get; set; }
        public string Html { get; set; }
        public int ReportFormId { get; set; }
    }
}
