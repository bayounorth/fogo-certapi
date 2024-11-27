namespace FogoCertApi.Repository.Models
{
    public class URLCategory : BaseModel
    {
        public string Name { get; set; }
        public List<UrlLink> Links { get; set; }

    }
}
