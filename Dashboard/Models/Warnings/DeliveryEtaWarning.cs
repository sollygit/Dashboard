using Dashboard.Models;
using System;

namespace Dashboard.Models.Warnings
{
    public class DeliveryEtaWarning : IWarning
    {
        public string Test(DeliveryOrder order)
        {
            if (order.FulfilmentType.ToLower() != "delivery" || order.PickStatus == "Complete")
                return null;
            return order.RequestDate.ToUniversalTime().Date < DateTimeOffset.UtcNow.Date ? "ETA" : null;
        }
    }
}
