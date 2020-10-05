namespace AutomacaoApiMantis.Domain
{
    public class UserDomain
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessLevel { get; set; }
        public string Enabled { get; set; }
        public string Protected { get; set; }

        public UserDomain(string username, string password, string realName, string email, string accessLevel, string enabled, string @protected)
        {

            Username = username;
            Password = password;
            RealName = realName;
            Email = email;
            AccessLevel = accessLevel;
            Enabled = enabled;
            Protected = @protected;
        }

        public UserDomain()
        {
        }
    }
}