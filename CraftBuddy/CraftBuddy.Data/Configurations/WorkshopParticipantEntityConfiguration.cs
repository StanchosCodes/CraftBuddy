using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class WorkshopParticipantEntityConfiguration : IEntityTypeConfiguration<WorkshopParticipant>
	{
		public void Configure(EntityTypeBuilder<WorkshopParticipant> builder)
		{
			builder.HasKey(wp => new { wp.WorkshopId, wp.ParticipantId });

			builder
				.HasOne(wp => wp.Workshop)
				.WithMany(w => w.Participants)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(wp => wp.Participant)
				.WithMany(p => p.JoinedWorkshops)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
