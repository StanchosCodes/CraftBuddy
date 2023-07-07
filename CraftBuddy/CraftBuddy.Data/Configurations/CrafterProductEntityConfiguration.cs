using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class CrafterProductEntityConfiguration : IEntityTypeConfiguration<UserProduct>
	{
		public void Configure(EntityTypeBuilder<UserProduct> builder)
		{
			builder.HasKey(cp => new { cp.MakerId, cp.ProductId });

			builder
				.HasOne(cp => cp.Maker)
				.WithMany(c => c.Products)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
