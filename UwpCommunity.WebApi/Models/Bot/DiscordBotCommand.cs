using System.Linq;

namespace UwpCommunity.WebApi.Models.Bot
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