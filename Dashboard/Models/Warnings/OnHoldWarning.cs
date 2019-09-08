using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models;

namespace Dashboard.Models.Warnings
{
    public class OnHoldWarning : IWarning
    {
        public string Test(DeliveryOrder order)
        {
            return order.HoldReleaseFlag ? "OnHold" : null;
        }
    }
}
