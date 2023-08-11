using System.Threading.Tasks;
using Delivery.Interfaces;

namespace Delivery.DHL
{
    public class DeliveryService : IDeliveryService
    {
        public decimal CalculateDeliveryCost(double weight)
        {
            return (decimal)(weight * 10);
        }

        public Task<bool> IsDeliveredAsync(int orderId)
        {
            return Task.FromResult(orderId % 2 == 0);
        }
    }
}