using System.Linq;
using Domain.Models;
using DomainServices.Interfaces;

namespace DomainServices.Implementation
{
    public class OrderDomainService : IOrderDomainService
    {
        public decimal GetTotal(Order order, CalculateDeliveryCost deliveryCostCalculator)
        {
            var totalPrice = order.Items.Sum(i => i.Quantity * i.Product.Price);
            if (totalPrice >= 1000) 
                return totalPrice;
            
            var totalWeight = order.Items.Sum(i => i.Quantity * i.Product.Weight);
            var deliveryCost = deliveryCostCalculator(totalWeight);
            
            return totalPrice + deliveryCost;
        }
    }
}