using Dashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.Interfaces
{
    public interface ISourceRepository
    {
        Task<IEnumerable<Source>> GetAll();
    }
}
