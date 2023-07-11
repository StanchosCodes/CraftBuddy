using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
	{
		public void Configure(EntityTypeBuilder<Purchase> builder)
		{
			builder
				.HasOne(p => p.Client)
				.WithMany(c => c.Purchases)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
