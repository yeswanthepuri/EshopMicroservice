




namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateorderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //Create Order Entity for Commanf Object
            //Save DB
            //return
            //
            var order = CreateNewOrder(command.order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }
        private Order CreateNewOrder(OrderDto orderdto)
        {
            var shippingAddress = Address.Of(orderdto.ShippingAddress.FirstName, orderdto.ShippingAddress.LastName, orderdto.ShippingAddress.EmailAddress, orderdto.ShippingAddress.AddressLine, orderdto.ShippingAddress.Country, orderdto.ShippingAddress.City, orderdto.ShippingAddress.State, orderdto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(orderdto.ShippingAddress.FirstName, orderdto.ShippingAddress.LastName, orderdto.ShippingAddress.EmailAddress, orderdto.ShippingAddress.AddressLine, orderdto.ShippingAddress.Country, orderdto.ShippingAddress.City, orderdto.ShippingAddress.State, orderdto.ShippingAddress.ZipCode);

            var newOrder = Order.Create(
                orderId: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderdto.CustomerId),
                orderName: OrderName.Of(orderdto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(orderdto.Payment.CardName, orderdto.Payment.CardNumber, orderdto.Payment.Expiration, orderdto.Payment.Cvv, orderdto.Payment.PaymentMethod));
            foreach (var orderItems in orderdto.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItems.ProductId), orderItems.Quantity, orderItems.Price);
            }
            return newOrder;
        }
    }
}
