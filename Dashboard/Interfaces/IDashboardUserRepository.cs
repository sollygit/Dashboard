using System.Collections.Generic;
using System.Security.Claims;

namespace Dashboard.Interfaces
{
    public interface IDashboardUserRepository
    {
        IEnumerable<int> GetBranchIds(ClaimsPrincipal principal);
    }
}
