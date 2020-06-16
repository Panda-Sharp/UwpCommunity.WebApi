using System.Linq;

namespace UwpCommunity.WebApi.Models.Discord
{
    public class DiscordBotCommand
    {
        public DiscordBotCommand(string message)
        {
            var command = message.Split("!");

            var parameters = command[1].Split(" ");

            switch (parameters[0])
            {
                case "ping":
                    Command = Commands.Ping;
                    break;
                case "user":
                    Command = Commands.User;
                    break;
            }

            if(parameters.Count() > 1)
            {
                Parameter = parameters[1];
            }
        }

        public Commands Command { get; set; }
        public string Parameter { get; set; }
    }
}
