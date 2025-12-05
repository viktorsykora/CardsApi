using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Api.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) 
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {            
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKeyHeaderValue))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing API key"));
            }

            if (!apiKeyHeaderValue.Equals("0de0cd7e-573f-446d-83ab-740ed6076200")) // Only for demo purposes,
                                                                                   // usually would be in secrets manager like Azure KeyVault                                                                                   
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "ApiKeyUser")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}