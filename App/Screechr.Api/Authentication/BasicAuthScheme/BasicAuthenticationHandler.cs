using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Screechr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Screechr.Api.Authentication.BasicAuthScheme
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthSchemeOptions>
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;
        public BasicAuthenticationHandler(
      IOptionsMonitor<BasicAuthSchemeOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock,
      ICustomAuthenticationManager customAuthenticationManager
      )
      : base(options, logger, encoder, clock)
        {
            _customAuthenticationManager = customAuthenticationManager;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            const string AuthHeaderKey = "Authorization";
            if (!Request.Headers.ContainsKey(AuthHeaderKey))
            {
                return AuthenticateResult.NoResult();
            }
            string tokenValue = Request.Headers[AuthHeaderKey];
            var result = await _customAuthenticationManager.ValidateToken(tokenValue?.Replace("Bearer ", string.Empty));
            if (result.IsAuthenticated)
            {
                var ticket = GetAuthenticationTicket(result);
                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Unauthorized");
        }
        private AuthenticationTicket GetAuthenticationTicket(AuthResult user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.Name}"),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, Scheme.Name);

        }
    }
}
