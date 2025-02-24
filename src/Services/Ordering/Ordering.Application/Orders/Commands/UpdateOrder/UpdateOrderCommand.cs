



namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto order):ICommand<UpdateOrderResult>;
   
    public record UpdateOrderResult(bool IsSuccess);


    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.order.OrderItems).NotEmpty().WithMessage("Order Items Should not be empty");
            RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("CustomerId is Required");
            RuleFor(x => x.order.OrderName).NotEmpty().WithMessage("Order Name is Required");
        }
    }
}
