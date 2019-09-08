using Dashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> Get(int locationId);
        Task<IEnumerable<Location>> GetBranches();
    }
}
