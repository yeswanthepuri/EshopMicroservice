

using Ordering.Domain.ValueObjects;
using System.Runtime.InteropServices;

namespace Ordering.Domain.Models
{
    public class Order : AggregateBase<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;

        public OrderName OrderName { get; private set; } = default!;
        //Value Objects
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;

        //Enum
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        

        public static Order Create(
            OrderId orderId,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment
            )
        {
            var order = new Order()
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDominaEvent(new OrderCreateEvent(order));

            return order;
        }
        public void  Update(
           CustomerId customerId,
           OrderName orderName,
           Address shippingAddress,
           Address billingAddress,
           Payment payment,
           OrderStatus orderStatus
           )
        {

            CustomerId = customerId;
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = orderStatus;

            AddDominaEvent(new OrderUpdateEvent(this));
        }

        public void Add(ProducId producId,int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, producId, quantity, price);

            _orderItems.Add(orderItem);
        }
        public void Remove(ProducId producId)
        {
            var orderItem = _orderItems.FirstOrDefault(x=>x.ProductId == producId);
            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
