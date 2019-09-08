using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Models;

namespace Dashboard.Models.Warnings
{
    public class StockOnHandWarning : IWarning
    {
        public string Test(DeliveryOrder order)
        {
            return order.Lines.Any(l => l.StockOnHand + l.QuantityPicked < l.Quantity) ? "StockOnHand" : null;
        }
    }
}