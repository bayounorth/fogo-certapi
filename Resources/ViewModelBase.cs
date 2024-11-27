namespace FogoCertApi.Resources
{
    public class ViewModelBase
    {
        public int Id { get; set; }
        public int EntityStateId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
