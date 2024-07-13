using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Domain.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.AddressId);
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.District).IsRequired().HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Detail).IsRequired().HasMaxLength(250);
    }
}