using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;
using System.Formats.Asn1;

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
        public class StoreBasketCommandHandler
            (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto) : 
            ICommandHandler<StoreBasketCommand, StoreBasketResult>
        {
            public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
            {
                await DeducatDiscount(command.Cart, cancellationToken);

                ShoppingCart cart = command.Cart;
                await basketRepository.StoreBasket(cart, cancellationToken);
                //TODO cache

                return new StoreBasketResult(cart.UserName);
            }

            private async Task DeducatDiscount(ShoppingCart cart, CancellationToken cancellationToken)
            {
                foreach (var item in cart.Items)
                {
                    var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                    item.Price -= coupon.Amount;
                }
            }
        }
    }
}
