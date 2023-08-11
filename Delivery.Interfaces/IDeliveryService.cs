using System.Threading.Tasks;

namespace Delivery.Interfaces
{
    public interface IDeliveryService
    {
        decimal CalculateDeliveryCost(double weight);
        Task<bool> IsDeliveredAsync(int orderId);
    }
}