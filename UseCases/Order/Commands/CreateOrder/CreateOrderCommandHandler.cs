using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Email.Interfaces;
using MediatR;
using WebApp.Interfaces;

namespace UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobService _jobService;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderCommandHandler(
            IDbContext dbContext,
            IMapper mapper,
            IBackgroundJobService jobService,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jobService = jobService;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Domain.Models.Order>(command.Dto);
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            _jobService.Schedule<IEmailService>(service 
                => service.SendAsync(_currentUserService.Email, "Order created", "Thanks"));
            
            return order.Id;
        }
    }
}