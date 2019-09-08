using Dashboard.Models;
using System.Threading.Tasks;

namespace Dashboard.Interfaces
{
    public interface IDeliveryOrderRepository
    {
        Task Upsert(DeliveryOrder order);
        Task CleanUp();
    }
}