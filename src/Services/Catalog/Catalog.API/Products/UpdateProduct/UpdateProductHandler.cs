using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;
using Marten;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal? Price) :
        ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product is null)
            {
               throw new ProductNotFoundException();
            }
            if (request.Category is not null)
            {
                product.Category = request.Category;
            }
            if (request.Description is not null)
            {
                product.Description = request.Description;
            }
            if (!string.IsNullOrEmpty(request.ImageFile))
            {
                product.ImageFile = request.ImageFile;
            }
            if(request.Price is not null)
            {
                product.Price = request.Price.Value;
            }

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);

        }
    }
}
