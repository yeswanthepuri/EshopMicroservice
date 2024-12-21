﻿using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsQuery(): IQuery <GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(
        IDocumentSession session,
        ILogger<GetProductsQueryHandler> logger) : 
        IQueryHandler<GetProductsQuery,GetProductsResult>
    {
       
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
