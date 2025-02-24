

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrderBynameResult>;

    public record GetOrderBynameResult(IEnumerable<OrderDto> Orders);

}
