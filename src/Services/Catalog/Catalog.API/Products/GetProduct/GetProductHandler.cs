using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);


    public  class GetProductHandler(
          IDocumentSession session,
        ILogger<GetProductHandler> logger) :
        IQueryHandler<GetProductByIdQuery, GetProductByIdResult> 
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id,cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
