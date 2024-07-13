using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Domain.Configuration;

public class OrderingConfiguration : IEntityTypeConfiguration<Ordering>
{
    public void Configure(EntityTypeBuilder<Ordering> builder)
    {
        builder.HasKey(o => o.OrderingId);
        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(o => o.OrderDate).IsRequired();

        builder.HasMany(o => o.OrderDetails)
            .WithOne(od => od.Ordering)
            .HasForeignKey(od => od.OrderingId);
    }
}