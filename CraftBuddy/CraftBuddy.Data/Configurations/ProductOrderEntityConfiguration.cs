﻿using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CraftBuddy.Data.Configurations
{
    internal class ProductOrderEntityConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(wp => new { wp.ProductId, wp.OrderId });

            builder
                .HasOne(wp => wp.Order)
                .WithMany(o => o.Products)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(wp => wp.Product)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
