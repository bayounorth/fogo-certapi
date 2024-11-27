namespace FogoCertApi.Repository.Models
{
    public class UrlLink : BaseModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int CategoryId { get; set; }

    }
}
