using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data;
using Yugen.Toolkit.Standard.Data.Interfaces;

namespace UwpCommunity.Data.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Result<User> SingleByDiscordId(string discordId);
    }
}
