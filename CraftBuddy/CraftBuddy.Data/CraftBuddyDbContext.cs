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
		public DbSet<Article> Articles { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

		public DbSet<ProductType> ProductTypes { get; set; } = null!;

		public DbSet<Product> Products { get; set; } = null!;

		public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;

		public DbSet<Workshop> Workshops { get; set; } = null!;

		public DbSet<WorkshopParticipant> WorkshopsParticipants { get; set; } = null!;

		public DbSet<ProductOrder> ProductsOrders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
		{
			Assembly configurationAssembly = Assembly.GetAssembly(typeof(CraftBuddyDbContext)) ??
														  Assembly.GetExecutingAssembly();

			builder.ApplyConfigurationsFromAssembly(configurationAssembly);

			base.OnModelCreating(builder);
		}
	}
}