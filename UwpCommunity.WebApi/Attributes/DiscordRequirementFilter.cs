using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using UwpCommunity.WebApi.Factories;
using UwpCommunity.WebApi.Interfaces;

namespace UwpCommunity.WebApi.Attributes
{
    public class DiscordRequirementFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jWToken = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();

            if (string.IsNullOrEmpty(jWToken))
            {
                context.Result = new ForbidResult();
                return;
            }

            var httpClientService = ServiceProviderFactory.ServiceProvider.GetService<IDiscordHttpClientService>();
            var result = httpClientService.GetDiscordUser(jWToken);

            if (!result.IsSuccess)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
