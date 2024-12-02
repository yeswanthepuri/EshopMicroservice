using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.GetProducts;
using Marten;
using Marten.Linq.QueryHandlers;
using System.Linq;

namespace Catalog.API.Products.GetProductsByCatagory
{
    public record GetProductsByCatagoryQuery(string catagory) : IQuery<GetProductsByCatagoryResult>;
    public record GetProductsByCatagoryResult(IEnumerable<Product> Products);
    public class GetProductsByCatagoryHandler(IDocumentSession session, ILogger<GetProductsByCatagoryHandler> logger) : IQueryHandler<GetProductsByCatagoryQuery, GetProductsByCatagoryResult>
    {
        public async Task<GetProductsByCatagoryResult> Handle(GetProductsByCatagoryQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(x=>x.Category.Contains(request.catagory)).ToListAsync(cancellationToken);
            return new GetProductsByCatagoryResult(products);
        }
    }
}
