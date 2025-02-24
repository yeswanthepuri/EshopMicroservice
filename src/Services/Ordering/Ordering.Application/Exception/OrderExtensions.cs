

namespace Ordering.Application.Exception
{
    public static class Extension
    {
        public static List<OrderDto> OrderToOrderDto(this IEnumerable<Order> orders)
        {
            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new OrderDto(
               Id: order.Id.Value,
               CustomerId: order.CustomerId.Value,
               OrderName: order.OrderName.Value,
               ShippingAddress: new AddressDto(
                   order.ShippingAddress.FirstName,
                   order.ShippingAddress.LastName,
                   order.ShippingAddress.EmailAddress,
                   order.ShippingAddress.AddressLine,
                   order.ShippingAddress.Country,
                   order.ShippingAddress.City,
                   order.ShippingAddress.State,
                   order.ShippingAddress.ZipCode
                   ),
               BillingAddress: new AddressDto(
                   order.BillingAddress.FirstName,
                   order.BillingAddress.LastName,
                   order.BillingAddress.EmailAddress,
                   order.BillingAddress.AddressLine,
                   order.BillingAddress.Country,
                   order.BillingAddress.City,
                   order.BillingAddress.State,
                   order.BillingAddress.ZipCode
                   ),
               Payment: new PaymentDto(order.Payment.CardName,
               order.Payment.CardNumber,
               order.Payment.CardExpiration,
               order.Payment.CVV,
               order.Payment.PaymentMethod),
               Status: order.Status,
               OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value,
               oi.ProductId.Value,
               oi.Quantity,
               oi.Price
               )).ToList());
                orderDtos.Add(orderDto);
            }
            return orderDtos;
        }
    }

}
