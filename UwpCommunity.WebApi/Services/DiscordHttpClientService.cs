using System.Net.Http;
using System.Text.Json;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Discord;
using Yugen.Toolkit.Standard.Core.Models;

namespace UwpCommunity.WebApi.Services
{
    public class DiscordHttpClientService : IDiscordHttpClientService
    {
        private readonly string url = "https://discordapp.com/api";
        private readonly HttpClient httpClient = new HttpClient();

        public Result<DiscordUser> GetDiscordUser(string accessToken)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

            var response = httpClient.GetAsync($"{url}/v6/users/@me").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var discordUser = JsonSerializer.Deserialize<DiscordUser>(content);
            return Result.IsOk(response.IsSuccessStatusCode, discordUser, "");
        }
    }
}
