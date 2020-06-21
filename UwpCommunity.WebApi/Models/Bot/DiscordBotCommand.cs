using System.Collections.Generic;
using System.Linq;
using UwpCommunity.WebApi.BotCommands;
using UwpCommunity.WebApi.Interfaces;

namespace UwpCommunity.WebApi.Models.Bot
{
    public class DiscordBotCommand
    {
        public static Dictionary<string, IBotCommand> List = new Dictionary<string, IBotCommand>()
        {
            {"nping", new PingBotCommand() },
            {"ngetuser",new UserBotCommand() },
            {"nnews",new NewsBotCommand() },
            {"nroleinfo",new RoleInfoBotCommand() },
        };

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