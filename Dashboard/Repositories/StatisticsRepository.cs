using Dashboard.Interfaces;
using Dashboard.Models;
using Dashboard.Models.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Extensions
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DashboardContext _context;
        private readonly DeliveryOrdersMapper _mapper;

        public StatisticsRepository(DashboardContext context, DeliveryOrdersMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        private IQueryable<DeliveryOrder> GetDeliveryOrders(params int[] branchIds) {
            if (branchIds.Length == 0)
                return GetDeliveryOrders();
            return GetDeliveryOrders().Where(o => branchIds.Contains(o.BranchId));
        }

        private IQueryable<DeliveryOrder> GetDeliveryOrders() 
        {
            return _context.DeliveryOrders
                .Include(o => o.Location)
                .Include(o => o.Pickers)
                .Include(o => o.PackageNotes)
                .Include(o => o.Lines)
                .AsNoTracking();
        }

        public async Task<IEnumerable<T>> Get<T>(params int[] branchIds)
        {
            return _mapper.Map<T>(await GetDeliveryOrders(branchIds).ToListAsync());
        }

        public async Task<IEnumerable<T>> GetSince<T>(DateTimeOffset lastUpdate, params int[] branchIds)
        {
            var lastUpdateUtc = lastUpdate.ToUniversalTime();
            var orders = _context.DeliveryOrders
                .Where(o => o.LastUpdated >= lastUpdateUtc);
            if (branchIds.Length > 0)
                orders = orders.Where(o => branchIds.Contains(o.BranchId));
            return _mapper.Map<T>(
                await orders
                    .Include(o => o.Location)
                    .Include(o => o.Pickers)
                    .Include(o => o.PackageNotes)
                    .Include(o => o.Lines)
                    .AsNoTracking()
                    .ToListAsync()
            );
        }
    }
}