using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enities;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Status)
                .HasDefaultValue(EOrderStatus.New)
                .IsRequired();
        }
    }
}