


using Ordering.Application.Exception;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrderBynameResult>
    {
        public async Task<GetOrderBynameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(x => x.OrderName.Value.Contains(query.Name))
                .ToListAsync(cancellationToken);

            var orderDtos = orders.OrderToOrderDto();
            return new GetOrderBynameResult(orderDtos);
        }

        
    }
}
