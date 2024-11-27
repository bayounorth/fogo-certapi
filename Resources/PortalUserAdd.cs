using FogoCertApi.Repository.Models;

namespace FogoCertApi.Resources
{
    public class PortalUserAdd : Authentication
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool isAdministrator { get; set; }
        public bool usesCompliance { get; set; }

    }
}
