using CraftBuddy.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CraftBuddy.Data
{
    public class CraftBuddyDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public CraftBuddyDbContext(DbContextOptions<CraftBuddyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Set> Sets { get; set; } = null!;

		public DbSet<Purchase> Purchases { get; set; } = null!;

		public DbSet<ProductType> ProductTypes { get; set; } = null!;

		public DbSet<Product> Products { get; set; } = null!;

		public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;

		public DbSet<Event> Events { get; set; } = null!;

		public DbSet<CustomOrder> CustomOrders { get; set; } = null!;

		public DbSet<UserProduct> UsersProducts { get; set; } = null!;

		public DbSet<UserEvent> UsersEvents { get; set; } = null!;

		public DbSet<UserPurchase> UsersPurchases { get; set; } = null!;

		public DbSet<UserCustomOrder> UsersCustomOrders { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			Assembly configurationAssembly = Assembly.GetAssembly(typeof(CraftBuddyDbContext)) ??
														  Assembly.GetExecutingAssembly();

			builder.ApplyConfigurationsFromAssembly(configurationAssembly);

			base.OnModelCreating(builder);
		}
	}
}