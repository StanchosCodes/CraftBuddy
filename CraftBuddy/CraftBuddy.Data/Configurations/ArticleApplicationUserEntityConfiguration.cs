using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class ArticleApplicationUserEntityConfiguration : IEntityTypeConfiguration<ArticleApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ArticleApplicationUser> builder)
		{
			builder.HasKey(aau => new { aau.ArticleId, aau.ApplicationUserId });

			builder
				.HasOne(aau => aau.ApplicationUser)
				.WithMany(au => au.LikedArticles)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(aau => aau.Article)
				.WithMany(a => a.UsersLikes)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
