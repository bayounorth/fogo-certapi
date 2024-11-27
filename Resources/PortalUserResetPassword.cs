namespace FogoCertApi.Resources
{
    public class PortalUserResetPassword
    {
        public string user_name { get; set; }
        public string token { get; set; }
        public string new_password { get; set; }
    }
}
