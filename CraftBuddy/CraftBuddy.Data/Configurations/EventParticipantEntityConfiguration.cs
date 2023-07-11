using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class EventParticipantEntityConfiguration : IEntityTypeConfiguration<EventParticipant>
	{
		public void Configure(EntityTypeBuilder<EventParticipant> builder)
		{
			builder.HasKey(ce => new { ce.EventId, ce.OrganiserId });

			builder
				.HasOne(ep => ep.Event)
				.WithMany(e => e.Participants)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
