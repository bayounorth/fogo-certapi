using System.ComponentModel.DataAnnotations;

namespace FogoCertApi.Resources
{
    public class PortalUserUpdate
    {
        [Required]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool isAdministrator { get; set; }
    }
}
