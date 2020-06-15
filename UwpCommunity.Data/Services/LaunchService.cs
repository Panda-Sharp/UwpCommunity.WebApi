using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Core.Models;

namespace UwpCommunity.Data.Services
{
    public class LaunchService : BaseService<Launch>, ILaunchService
    {
        public LaunchService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Result<IEnumerable<Launch>> GetProjectsByLaunchYear(string year) =>
            Get(launch => launch.Year.Equals(year),
                x => x.Include(launch => launch.LaunchProjects)
                    .ThenInclude(launchProject => launchProject.Project)
                        .ThenInclude(project => project.Category));

        public Result<Launch> SingleByLaunchYear(string year) =>
            Single(x => x.Year.Equals(year));
    }
}
