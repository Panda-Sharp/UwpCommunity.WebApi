using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;

namespace UwpCommunity.WebApi.Auth
{
    public class DiscordAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IDiscordHttpClientService _discordHttpClientService;

        public DiscordAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
          UrlEncoder encoder, ISystemClock clock, IDiscordHttpClientService discordHttpClientService)
          : base(options, logger, encoder, clock)
        {
            _discordHttpClientService = discordHttpClientService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var jWToken = Request.Headers[HeaderNames.Authorization].ToString();

            if (string.IsNullOrEmpty(jWToken))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            var result = _discordHttpClientService.GetDiscordUser(jWToken);

            if (!result.IsSuccess)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }

            var claims = new[] { new Claim("", "") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
