using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Core.Models;

namespace UwpCommunity.Data.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Result<User> SingleByDiscordId(string discordId) =>
            Single(user => user.DiscordId.Equals(discordId),
                x => x.Include(user => user.UserProjects)
                 .ThenInclude(userProject => userProject.Project)
                    .ThenInclude(project => project.Category));

        public Result<IEnumerable<User>> GetProjectsByByDiscordId(string discordId) =>
            Get(user => user.DiscordId.Equals(discordId),
                x => x.Include(user => user.UserProjects)
                 .ThenInclude(userProject => userProject.Project)
                    .ThenInclude(project => project.Category));
    }
}
