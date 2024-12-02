using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.GetProduct;
using Catalog.API.Products.UpdateProduct;
using Marten;

namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(Boolean IsSuccess);
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Name is required");

        }
    }
    public class DeleteProductHandler(IDocumentSession session,
        ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync();
            return new DeleteProductResult(true);
        }
    }
}
