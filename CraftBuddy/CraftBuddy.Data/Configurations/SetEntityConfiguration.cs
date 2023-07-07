using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class SetEntityConfiguration : IEntityTypeConfiguration<Set>
	{
		public void Configure(EntityTypeBuilder<Set> builder)
		{
			builder
				.HasOne(s => s.Maker)
				.WithMany(m => m.Sets)
				.HasForeignKey(s => s.MakerId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
