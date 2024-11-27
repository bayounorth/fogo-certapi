namespace FogoCertApi.Repository.Models
{
    public class BaseModel
    {
        public int id { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int EntityStateId { get; set; }
    }
}
