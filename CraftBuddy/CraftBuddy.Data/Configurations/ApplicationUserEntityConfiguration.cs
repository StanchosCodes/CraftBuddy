using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CraftBuddy.Common.EntityValidationConstants.ApplicationUser;

namespace CraftBuddy.Data.Configurations
{
	public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.Property(au => au.Email)
				.HasMaxLength(EmailMaxLength)
				.IsRequired(true);

			builder
				.Property(au => au.UserName)
				.HasMaxLength(UserNameMaxLength)
				.IsRequired(true);
		}
	}
}
