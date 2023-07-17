using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.HasOne(p => p.Client)
				.WithMany(c => c.Orders)
				.OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Products);
        }
	}
}
