using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Net.Http;

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

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", jWToken);
            var response = httpClient.GetAsync("https://discordapp.com/api/v6/users/@me").Result;

            if (!response.IsSuccessStatusCode)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
