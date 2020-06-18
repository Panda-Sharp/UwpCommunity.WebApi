namespace UwpCommunity.WebApi.Models.Discord
{
    public class DiscordUserDto
    {
        public DiscordUserDto(DSharpPlus.Entities.DiscordUser resultJson)
        {
            Email = resultJson.Email;
            Id = resultJson.Id;
            Username = resultJson.Username;
        }

        public string Email { get; set; }
        public ulong Id { get; set; }
        public string Username { get; set; }
    }
}
