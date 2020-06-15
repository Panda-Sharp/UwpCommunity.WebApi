using System.Collections.Generic;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Core.Models;

namespace UwpCommunity.Data.Interfaces
{
    public interface ILaunchService : IBaseService<Launch>
    {
        Result<IEnumerable<Launch>> GetProjectsByLaunchYear(string year);

        Result<Launch> SingleByLaunchYear(string year);
    }
}
