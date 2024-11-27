namespace FogoCertApi.Repository.Models
{
    public class Vessel : BaseModel
    {
        public string Name { get; set; }
        public int VesselTypeId { get; set; }
        public int Ordinal { get; set; }

    }
}
