using System.Collections.Generic;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Core.Models;

namespace UwpCommunity.Data.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Result<User> SingleByDiscordId(string discordId);

        Result<IEnumerable<User>> GetProjectsByByDiscordId(string discordId);
    }
}
