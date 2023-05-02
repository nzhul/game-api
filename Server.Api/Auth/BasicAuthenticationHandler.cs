using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server.Common.Settings;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Server.Api.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BasicCredentials _credentials;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<BasicCredentials> creds)
            : base(options, logger, encoder, clock)
        {
            _credentials = creds.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization key");
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Authorization header does not start with 'Basic '");
            }

            var credBytes = Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase));
            var creds = Encoding.UTF8.GetString(credBytes);

            var authTokens = creds.Split(':', 2);

            if (authTokens.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Authorization header");
            }

            var clientId = authTokens[0];
            var clientSecret = authTokens[1];

            if (clientId != _credentials.Username || clientSecret != _credentials.Password)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            var client = new BasicAuthenticationClient
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = clientId
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                new Claim(ClaimTypes.Name, clientId)
            }));

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, BasicAuthenticationDefaults.AuthenticationScheme));
        }
    }
}
