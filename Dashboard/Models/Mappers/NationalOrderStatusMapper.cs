using Dashboard.Models;
using Dashboard.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Models.Mappers
{
    public class NationalOrderStatusMapper : IMapper
    {
        public Type ToType => typeof(NationalOrderStatus);
        private TimeZoneInfo timeZone;
        private string[] completeStatuses;

        public NationalOrderStatusMapper(TimeZoneSettings settings)
        {
            timeZone = TimeZoneInfo.FindSystemTimeZoneById(settings.Id);
            completeStatuses = new string[]
            {
                "packed",
                "collected",
                "delivered"
            };
        }

        public IEnumerable<object> Map(IEnumerable<DeliveryOrder> orders)
        {
            return orders.Aggregate(new List<NationalOrderStatus>(), (statuses, order) =>
            {
                var status = statuses.SingleOrDefault(s => s.FulfilmentType == order.FulfilmentType);
                if (status == null)
                {
                    status = new NationalOrderStatus
                    {
                        FulfilmentType = order.FulfilmentType,
                        CompletedOrders = 0
                    };
                    statuses.Add(status);
                }
                if (IsComplete(order.PickStatus) && order.PickStatusCompleteDateTime.HasValue)
                {
                    var today = DateTimeOffset.UtcNow.ToOffset(order.PickStatusCompleteDateTime.Value.Offset).Date;
                    if (order.PickStatusCompleteDateTime.Value.Date == today)
                        status.CompletedOrders++;
                }
                return statuses;
            });
        }

        private bool IsComplete(string status)
        {
            return completeStatuses.Any(s => s == status.ToLower());
        }
    }
}
