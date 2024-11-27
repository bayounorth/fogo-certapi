using FogoCertApi.Repository.Models;

namespace FogoCertApi.Resources
{
    public class PortalUsersViewModel : ApiResponseBase
    {
        public PortalUsersViewModel() { }
        public List<PortalUser> Result { get; set; }
    }
}
