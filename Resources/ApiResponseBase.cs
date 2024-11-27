using FogoCertApi.Repository.Models;

namespace FogoCertApi.Resources
{
    public class ApiResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ApiError Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }

    }
}
