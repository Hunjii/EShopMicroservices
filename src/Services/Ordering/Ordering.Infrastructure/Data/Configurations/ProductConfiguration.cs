﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                    productId => productId.Value,
                    dbId => ProductId.Of(dbId));
        }
    }
}
