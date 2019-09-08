using Dashboard.Models;

namespace Dashboard.Models.Warnings
{
    public interface IWarning
    {
        string Test(DeliveryOrder order);
    }
}