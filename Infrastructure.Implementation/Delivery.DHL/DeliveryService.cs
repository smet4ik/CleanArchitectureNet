using Delivery.Interfaces;

namespace Delivery.DHL
{
    public class DeliveryService : IDeliveryService
    {
        public decimal CalculateDeliveryCost(double weight)
        {
            return (decimal)(weight * 10);
        }
    }
}