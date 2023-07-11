using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class CustomOrderEntityConfiguration : IEntityTypeConfiguration<CustomOrder>
	{
		public void Configure(EntityTypeBuilder<CustomOrder> builder)
		{
			builder
				.HasOne(co => co.Client)
				.WithMany(c => c.CustomOrders)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
