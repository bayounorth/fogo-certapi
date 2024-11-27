namespace FogoCertApi.Repository.Models
{
    public class PortalUser
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string name { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? forgot_password_expire_time { get; set; }
        public string forgot_password_token { get; set; }
        public bool isAdministrator { get; set; }

    }
}
