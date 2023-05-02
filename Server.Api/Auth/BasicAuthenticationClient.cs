using System.Security.Principal;

namespace Server.Api.Auth
{
    public class BasicAuthenticationClient : IIdentity
    {
        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }
    }
}
