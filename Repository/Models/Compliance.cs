namespace FogoCertApi.Repository.Models
{
    public class Compliance: BaseModel
    {
        public string Name { get; set; }
        public List<Form> Forms { get; set; }
    }
}
