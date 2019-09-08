using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<T>> Get<T>(params int[] branchIds);
        Task<IEnumerable<T>> GetSince<T>(DateTimeOffset lastUpdate, params int[] branchIds);
    }
}