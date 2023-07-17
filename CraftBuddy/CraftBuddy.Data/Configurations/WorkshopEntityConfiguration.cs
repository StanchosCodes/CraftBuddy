using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
    public class WorkshopEntityConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder
                .HasOne(w => w.Organiser)
                .WithMany(o => o.Workshops)
                .HasForeignKey(w => w.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
