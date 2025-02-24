



namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto order):ICommand<CreateOrderResult>;
   
    public record CreateOrderResult(Guid Id);


    public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.order.OrderItems).NotEmpty().WithMessage("Order Items Should not be empty");
            RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("CustomerId is Required");
            RuleFor(x => x.order.OrderName).NotEmpty().WithMessage("Order Name is Required");
        }
    }
}
