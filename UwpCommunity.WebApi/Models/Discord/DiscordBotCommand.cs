using System.Linq;
using System.Net.NetworkInformation;

namespace UwpCommunity.WebApi.Models.Discord
{
    public class DiscordBotCommand
    {
        public DiscordBotCommand(string message)
        {
            var command = message.Split("!");
            var parameters = command[1].Split(" ");

            Command = parameters[0];

            if (parameters.Count() > 1)
            {
                Parameters = parameters.Skip(1).ToArray();
            }
        }

        public string Command { get; set; }
        public string[] Parameters { get; set; }
    }
}
