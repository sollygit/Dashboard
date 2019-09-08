using System.Linq;

namespace Dashboard.Models.Warnings
{
    public class BackOrderWarning : IWarning
    {
        public string Test(DeliveryOrder order)
        {
            return order.Lines != null && order.Lines.Any(l => l.BackOrder) ? "BackOrder" : null;
        }
    }
}
