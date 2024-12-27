using BuildingBlocks.CQRS;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteCommandValidation : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteCommandValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        }
        public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
        {
            public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
            {
                //TODO delete basket from DB and Cache

                return new DeleteBasketResult(true);
            }
        }
    }
}
