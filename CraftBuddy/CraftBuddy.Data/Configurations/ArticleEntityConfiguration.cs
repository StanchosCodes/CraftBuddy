using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class ArticleEntityConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder
				.HasOne(a => a.Crafter)
				.WithMany(w => w.Articles)
				.HasForeignKey(a => a.CrafterId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
