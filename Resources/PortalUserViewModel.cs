using FogoCertApi.Repository.Models;

namespace FogoCertApi.Resources
{
    public class PortalUserViewModel : ApiResponseBase
    {
        public PortalUserViewModel() { }
        public PortalUser Result { get; set; }
    }
}
