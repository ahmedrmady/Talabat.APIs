using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositry.Data.Config
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(Order => Order.ShippingAddress,
                ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(order => order.Status)
                   .HasConversion(
                OStauts => OStauts.ToString(),
                OStauts =>(OrderStatus) Enum.Parse(typeof(OrderStatus),OStauts)
                   );

            builder.HasOne(Order => Order.DeliveryMethod)
                   .WithMany();

            builder.Property(order => order.SubTotal)
                   .HasColumnType("decimal(18,2)");

        }
    }
}
