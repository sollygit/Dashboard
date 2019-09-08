using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models;

namespace Dashboard.Models.Warnings
{
    public class ParkedWarning : IWarning
    {
        public string Test(DeliveryOrder order)
        {
            return order.Lines.Any(l => l.Quantity == l.QuantityPicked) && order.PickStatus == "Part Picked" ? "Parked" : null;
        }
    }
}
