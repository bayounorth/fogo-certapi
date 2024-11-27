namespace FogoCertApi.Resources
{
    public class ApiError
    {
        public string message { get; set; }
        public string details { get; set; }
        public List<ApiValidationErrors> validationErrors { get; set; }
    }
}
