using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class OrderStatusEntityConfiguration : IEntityTypeConfiguration<OrderStatus>
	{
		public void Configure(EntityTypeBuilder<OrderStatus> builder)
		{
			builder.HasData(this.GenerateOrderStatuses());
		}

		private OrderStatus[] GenerateOrderStatuses()
		{
			ICollection<OrderStatus> orderStatuses = new HashSet<OrderStatus>();

			OrderStatus orderStatus;

			orderStatus = new OrderStatus()
			{
				Id = 1,
				Name = "Waiting"
			};

			orderStatuses.Add(orderStatus);

			orderStatus = new OrderStatus()
			{
				Id = 2,
				Name = "Crafting"
			};

			orderStatuses.Add(orderStatus);

			orderStatus = new OrderStatus()
			{
				Id = 3,
				Name = "Crafted"
			};

			orderStatuses.Add(orderStatus);

			return orderStatuses.ToArray();
		}
	}
}
