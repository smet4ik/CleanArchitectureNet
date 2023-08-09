using System.Linq;
using Domain.Entities;
using DomainServices.Interfaces;

namespace DomainServices.Implementation
{
    public class OrderDomainService : IOrderDomainService
    {
        public decimal GetTotal(Order order)
        {
            return order.Items.Sum(i => i.Quantity * i.Product.Price);;
        }
    }
}