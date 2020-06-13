using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Models;

namespace UwpCommunity.Data.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Result<User> SingleByDiscordId(string discordId) =>
            Single(x => x.DiscordId.Equals(discordId),
                x => x.Include(y => y.UserProjects)
                 .ThenInclude(z => z.Project));

        public Result<IEnumerable<User>> GetProjectsByByDiscordId(string discordId) =>
            Get(x => x.DiscordId.Equals(discordId),
                x => x.UserProjects);
    }
}
