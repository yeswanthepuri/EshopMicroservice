


using Microsoft.EntityFrameworkCore;
using Ordering.Application.Exception;
using Ordering.Application.Orders.Queries.GetOrderByName;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query
            , CancellationToken cancellationToken)
        {
            CustomerId customerId = CustomerId.Of(query.CustomerId);

            var orders = await dbContext.Orders
              .Include(o => o.OrderItems)
              .AsNoTracking()
              .Where(x => x.CustomerId == customerId)
              .ToListAsync(cancellationToken);

            var orderDtos = orders.OrderToOrderDto();
            return new GetOrderByCustomerResult(orderDtos);
        }
    }
}
