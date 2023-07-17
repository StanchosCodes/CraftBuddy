using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder
				.HasOne(p => p.Crafter)
				.WithMany(c => c.Products)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasMany(p => p.Orders);
		}
	}
}
