using System.Text.Json.Serialization;

namespace UwpCommunity.WebApi.Models.Discord
{
    public class DiscordUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
        [JsonPropertyName("discriminator")]
        public string Discriminator { get; set; }
        [JsonPropertyName("public_flags")]
        public int Public_flags { get; set; }
        [JsonPropertyName("flags")]
        public int Flags { get; set; }
        [JsonPropertyName("locale")]
        public string Locale { get; set; }
        [JsonPropertyName("mfa_enabled")]
        public bool MfaEnabled { get; set; }
    }
}
