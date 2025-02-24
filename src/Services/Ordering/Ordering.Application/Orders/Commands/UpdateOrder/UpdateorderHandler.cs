




using Ordering.Application.Exception;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateorderHandler(IApplicationDbContext dbContext) 
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
      
            var orderId = OrderId.Of(command.order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
            if (order == null)
            {
                throw new OrderNotFoundException(command.order.Id.ToString());
            }
            UpdateOrderWithNewValues(order,command.order);
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }
        private void UpdateOrderWithNewValues(Order order,OrderDto orderdto)
        {
            var shippingAddress = Address.Of(orderdto.ShippingAddress.FirstName, orderdto.ShippingAddress.LastName, orderdto.ShippingAddress.EmailAddress, orderdto.ShippingAddress.AddressLine, orderdto.ShippingAddress.Country, orderdto.ShippingAddress.City, orderdto.ShippingAddress.State, orderdto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(orderdto.BillingAddress.FirstName, orderdto.BillingAddress.LastName, orderdto.BillingAddress.EmailAddress, orderdto.BillingAddress.AddressLine, orderdto.BillingAddress.Country, orderdto.BillingAddress.City, orderdto.BillingAddress.State, orderdto.BillingAddress.ZipCode);
            var updatePayments = Payment.Of(orderdto.Payment.CardName, orderdto.Payment.CardNumber, orderdto.Payment.Expiration, orderdto.Payment.Cvv, orderdto.Payment.PaymentMethod);

            order.Update(orderName: OrderName.Of(orderdto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: updatePayments,
                orderStatus: orderdto.Status);
 
        }
    }
}
