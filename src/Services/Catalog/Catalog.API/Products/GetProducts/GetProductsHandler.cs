﻿using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery <GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(
        IDocumentSession session,
        ILogger<GetProductsQueryHandler> logger) : 
        IQueryHandler<GetProductsQuery,GetProductsResult>
    {
       
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(request.PageNumber ?? 1,request.PageSize ?? 10,cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
