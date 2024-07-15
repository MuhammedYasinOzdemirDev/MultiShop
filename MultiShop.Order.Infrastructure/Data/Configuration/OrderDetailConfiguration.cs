using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Domain.Configuration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(od => od.OrderDetailId);
        builder.Property(od => od.ProductId).IsRequired();
        builder.Property(od => od.ProductName).IsRequired().HasMaxLength(200);
        builder.Property(od => od.ProductPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(od => od.ProductAmount).IsRequired();
        builder.Property(od => od.ProductTotalPrice).IsRequired().HasColumnType("decimal(18,2)");

        builder.HasOne(od => od.Ordering)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderingId);
    }
}