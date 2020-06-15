using System.Collections.Generic;
using UwpCommunity.Data.Models;
using Yugen.Toolkit.Standard.Data.Interfaces;
using Yugen.Toolkit.Standard.Core.Models;

namespace UwpCommunity.Data.Interfaces
{
    public interface IProjectService : IBaseService<Project>
    {
        Result<IEnumerable<Project>> GetWithCategory();
    }
}
