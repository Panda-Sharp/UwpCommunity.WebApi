using System.Collections.Generic;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Models;

namespace UwpCommunity.Data.Services
{
    public class LaunchService : BaseService<Launch>, ILaunchService
    {
        public LaunchService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Result<IEnumerable<Launch>> GetProjectsByLaunchYear(string year) =>
            Get(x => x.Year.Equals(year));
    }
}
