using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class ClientCustomOrderEntityConfiguration : IEntityTypeConfiguration<UserCustomOrder>
	{
		public void Configure(EntityTypeBuilder<UserCustomOrder> builder)
		{
			builder.HasKey(cco => new { cco.CustomOrderId, cco.ClientId });

			builder
				.HasOne(cco => cco.Client)
				.WithMany(c => c.CustomOrders)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
