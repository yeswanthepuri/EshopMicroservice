

using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(o => o.Value,
                dbID => OrderId.Of(dbID));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)

                .IsRequired();

            builder.HasMany<OrderItem>(o=>o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(o => o.OrderName, nameBuilder => {
                nameBuilder.Property(n=>n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });
            builder.ComplexProperty(
                o => o.ShippingAddress, shippingAddressBuilder => {

                    shippingAddressBuilder.Property(a=>a.FirstName).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a=>a.LastName).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a=>a.EmailAddress).HasMaxLength(50);
                    shippingAddressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                    shippingAddressBuilder.Property(a => a.Country).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired();

                }
            );
            builder.ComplexProperty(
                o => o.BillingAddress, shippingAddressBuilder => {

                    shippingAddressBuilder.Property(a => a.FirstName).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a => a.LastName).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                    shippingAddressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                    shippingAddressBuilder.Property(a => a.Country).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired();
                    shippingAddressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired();

                }
            );
            builder.ComplexProperty(
               o => o.Payment, paymentBuilder => {

                   paymentBuilder.Property(a => a.CardName).HasMaxLength(50).IsRequired();
                   paymentBuilder.Property(a => a.CardNumber).HasMaxLength(24).IsRequired();
                   paymentBuilder.Property(a => a.CardExpiration).HasMaxLength(24).IsRequired();
                   paymentBuilder.Property(a => a.CVV).HasMaxLength(3).IsRequired();
                   paymentBuilder.Property(a => a.PaymentMethod);
                }
           );
            builder.Property(o => o.Status)
                                .HasDefaultValue(OrderStatus.Draft)
                                .HasConversion(x => x.ToString(), 
                                dbstatus => 
                                (OrderStatus)Enum.Parse(typeof(OrderStatus)
                                , dbstatus));
            builder.Property(o => o.TotalPrice);
        }
    }
}
