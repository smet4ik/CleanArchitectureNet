using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Delivery.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace UseCases.BackgroundJobs
{
    public class UpdateDeliveryStatusJob
    {
        private readonly IDbContext _dbContext;
        private readonly IDeliveryService _deliveryService;

        public UpdateDeliveryStatusJob(IDbContext dbContext, IDeliveryService deliveryService)
        {
            _dbContext = dbContext;
            _deliveryService = deliveryService;
        }

        public async Task ExecuteAsync()
        {
            var orders = await _dbContext.Orders
                .Where(o => o.Status == OrderStatus.Created)
                .ToListAsync();

            var orderStatuses = orders
                .Select(o => new {Order = o, DeliveryTask = _deliveryService.IsDeliveredAsync(o.Id)}).ToList();

            await Task.WhenAll(orderStatuses.Select(o => o.DeliveryTask));

            foreach (var orderStatus in orderStatuses)
            {
                if (!orderStatus.DeliveryTask.Result)
                    continue;
                
                orderStatus.Order.Status = OrderStatus.Delivered;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}