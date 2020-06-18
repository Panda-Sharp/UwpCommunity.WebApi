using System.Collections.Generic;
using UwpCommunity.WebApi.BotCommands;
using UwpCommunity.WebApi.Interfaces;

namespace UwpCommunity.WebApi.Models.Bot
{
    public static class Commands
    {
        public static Dictionary<string, IBotCommand> List = new Dictionary<string, IBotCommand>()
        {
            {"nping", new PingBotCommand() },
            {"ngetuser",new UserBotCommand() },
            //{"nnews",new NewsBotCommand() },
            {"nroleinfo",new RoleInfoBotCommand() },
        };
    }

}
