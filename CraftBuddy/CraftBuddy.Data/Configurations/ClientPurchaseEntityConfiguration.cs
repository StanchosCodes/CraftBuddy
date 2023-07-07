using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class ClientPurchaseEntityConfiguration : IEntityTypeConfiguration<UserPurchase>
	{
		public void Configure(EntityTypeBuilder<UserPurchase> builder)
		{
			builder.HasKey(cp => new { cp.PurchaseId, cp.UserId });

			builder
				.HasOne(cp => cp.User)
				.WithMany(c => c.Purchases)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
