using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitiateData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using (var session = store.LightweightSession())
            {
                if(await session.Query<Product>().AnyAsync())
                {
                    return;
                }
                session.Store<Product>(GetPreconfigurationProducts());
                await session.SaveChangesAsync();
            }
        }

        private IEnumerable<Product> GetPreconfigurationProducts() => new List<Product>()
        {
            new Product()
            {
                Name = "Samsung s24",
                Category = new List<string> { "Smart Phone" },
                Price =245666,
                Description ="Best phone with smart features",
                Id =  Guid.NewGuid(),
            },
            new Product()
            {
                Name = "14 pro max",
                Category = new List<string> { "Iphone Phone" },
                Price =1245666,
                Description ="Best value for your kidney",
                Id =  Guid.NewGuid(),
            }
        };
    }
}
