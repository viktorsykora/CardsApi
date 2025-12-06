using System.Security.Claims;
using System.Text.Encodings.Web;
using Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Api.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ICredentialsProvider _credentialsProvider;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ICredentialsProvider credentialsProvider) 
            : base(options, logger, encoder)
        {
            _credentialsProvider = credentialsProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {            
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKeyHeaderValue))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing API key"));
            }

            if (!_credentialsProvider.TryGetCredentials(apiKeyHeaderValue, out string userName)) 
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}