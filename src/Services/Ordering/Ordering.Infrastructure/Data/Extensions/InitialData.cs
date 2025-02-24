using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>()
            {
                Customer.Create(CustomerId.Of(new Guid("4e2c1be5-ec1d-4425-b406-a1129290cfd4")),"Yeswanth","admin@marthand.in"),
                Customer.Create(CustomerId.Of(new Guid("e5c81026-e125-49f5-8c0b-2a83fb052e25")),"Gayatri","hr@marthand.in"),
                Customer.Create(CustomerId.Of(new Guid("fe3feec3-ee42-420a-8d24-342a97f5a929")),"SaiRakshih","hr1@marthand.in")
            };

        public static IEnumerable<Product> Products =>
           new List<Product>()
           {
               Product.Create(ProductId.Of(new Guid("e93fa6a7-f100-47f3-9109-a1da0ed6b33f")),"IPhone 16",69_000),
               Product.Create(ProductId.Of(new Guid("a1de53d0-db0c-47c6-a97e-a035c89a9dcf")),"Samung S25 Plus",89_000),
               Product.Create(ProductId.Of(new Guid("8199760c-49c7-4fec-a5e7-61ce220b88e4")),"Samung Charger",2_000),
           };

        public static IEnumerable<Order> OrderwithItems
        {
            get
            {
                var address1 = Address.Of("Yeswanth", "Epuri", "admin@marthand.in", "t2 - 410, Ramky one galaxia", "India", "Hyderabad", "Telangana", "500019");
                var address2 = Address.Of("SaiRakshih", "Epuri", "hr1@marthand.in", "t2 - 410, Ramky one galaxia", "India", "Hyderabad", "Telangana", "500019");

                var payment1 = Payment.Of("E.V.Yeswanth", "4200 0000 0000 0001", "12/30", "722", 1);
                var payment2 = Payment.Of("E.V.Sai Rakshith", "4500 0000 0000 0004", "12/35", "850", 1);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("4e2c1be5-ec1d-4425-b406-a1129290cfd4")),
                    OrderName.Of("YesORD"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);
                order1.Add(ProductId.Of(new Guid("e93fa6a7-f100-47f3-9109-a1da0ed6b33f")), 2, 59_000);

                var order2 = Order.Create(
                   OrderId.Of(Guid.NewGuid()),
                   CustomerId.Of(new Guid("fe3feec3-ee42-420a-8d24-342a97f5a929")),
                   OrderName.Of("RakORD"),
                   shippingAddress: address2,
                   billingAddress: address2,
                   payment2);
                order2.Add(ProductId.Of(new Guid("a1de53d0-db0c-47c6-a97e-a035c89a9dcf")), 1, 69_000);
                order2.Add(ProductId.Of(new Guid("8199760c-49c7-4fec-a5e7-61ce220b88e4")), 1, 2_000);

                return new List<Order> { order1, order2 };
            }
            
        }

    }
}
