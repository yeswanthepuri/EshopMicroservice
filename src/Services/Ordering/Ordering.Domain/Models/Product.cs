


namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        protected Product()
        {

        }
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;

        public static Product Create(ProductId producId, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            var product = new Product
            {
                Id = producId,
                Name = name,
                Price = price
            };
            return product;
        }
    }
}
