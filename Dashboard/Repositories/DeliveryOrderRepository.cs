using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Extensions
{
    public class DeliveryOrderRepository : IDeliveryOrderRepository
    {
        private readonly DashboardContext _context;
        private readonly ILocationRepository locationRepository;

        public DeliveryOrderRepository(DashboardContext context, ILocationRepository locationRepository)
        {
            _context = context;
            this.locationRepository = locationRepository;
        }

        public async Task Upsert(DeliveryOrder order)
        {
            order.LastUpdated = DateTimeOffset.UtcNow;
            var originalOrder = _context.DeliveryOrders
              .SingleOrDefault(o => o.DeliveryOrderId == order.DeliveryOrderId
                && o.BranchId == order.BranchId
                && o.TransCode == order.TransCode);
            if (originalOrder != null)
            {
                _context.DeliveryOrders.Remove(originalOrder);
                await _context.SaveChangesAsync();
            }
            order.Location = await locationRepository.Get(order.BranchId);
            _context.DeliveryOrders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task CleanUp()
        {
            var updateDate = DateTimeOffset.UtcNow.AddMinutes(1);
            await _context.Database.ExecuteSqlCommandAsync("delete from DeliveryOrders where PickStatus = 'Cancelled'");
            await _context.Database.ExecuteSqlCommandAsync("update DeliveryOrders set PickStatus = 'Cancelled', LastUpdated = @updateDate", new SqlParameter("@updateDate", updateDate));
        }
    }
}