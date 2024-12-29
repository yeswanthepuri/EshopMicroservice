using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoredBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoredBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can't be null");
            RuleFor(x => x.Cart.UserName).NotNull().WithMessage("User is Required");
        }
        public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
        {
            public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
            {
                ShoppingCart cart = command.Cart;
                await basketRepository.StoreBasket(cart, cancellationToken);
                //TODO cache
                return new StoreBasketResult(cart.UserName);
            }
        }
    }
}
