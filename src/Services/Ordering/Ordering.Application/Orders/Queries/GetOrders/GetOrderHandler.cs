


using BuildingBlocks.Pagination;
using Ordering.Application.Exception;


namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrderHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
    {
        public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageindex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var count = await dbContext.OrderItems.LongCountAsync(cancellationToken);
            var orders = await dbContext.Orders
             .Include(o => o.OrderItems)
             .AsNoTracking()
             .Skip(pageSize * pageindex)
             .Take(pageSize)
             .ToListAsync(cancellationToken);

            var orderDtos = orders.OrderToOrderDto();
            return new GetOrdersQueryResult(new PaginatedResult<OrderDto>(pageindex,pageSize,count, orderDtos));
        }
    }
}
