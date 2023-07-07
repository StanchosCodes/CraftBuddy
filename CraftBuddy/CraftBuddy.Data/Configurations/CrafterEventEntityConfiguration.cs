using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class CrafterEventEntityConfiguration : IEntityTypeConfiguration<UserEvent>
	{
		public void Configure(EntityTypeBuilder<UserEvent> builder)
		{
			builder.HasKey(ce => new { ce.EventId, ce.OrganiserId });

			builder
				.HasOne(ce => ce.Organiser)
				.WithMany(c => c.Events)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
