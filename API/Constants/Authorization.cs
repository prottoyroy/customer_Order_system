namespace API.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }
        public const string default_username = "defaultUser";
        public const string default_email = "defaultUser@gmail.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.Administrator;
        
}
}