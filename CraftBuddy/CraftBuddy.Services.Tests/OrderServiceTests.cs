using CraftBuddy.Data;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Services.Data;
using Microsoft.EntityFrameworkCore;
using static CraftBuddy.Services.Tests.DatabaseSeeder;
using CraftBuddy.Web.ViewModels.Product;
using CraftBuddy.Web.ViewModels.Order;

namespace CraftBuddy.Services.Tests
{
	public class OrderServiceTests
	{
		private DbContextOptions<CraftBuddyDbContext> contextOptions;
		private CraftBuddyDbContext context;
		private CraftBuddyDbContext contextB;
		private IOrderService orderService;
		private IOrderService orderServiceB;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.contextOptions = new DbContextOptionsBuilder<CraftBuddyDbContext>()
				.UseInMemoryDatabase("CraftBuddyInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.context = new CraftBuddyDbContext(this.contextOptions);
			this.contextB = new CraftBuddyDbContext(this.contextOptions);

			//this.context.Database.EnsureDeleted();
			this.context.Database.EnsureCreated();

			SeedDatabase(this.context);

			this.orderService = new OrderService(this.context);
			this.orderServiceB = new OrderService(this.contextB);
		}

		//[Test]
		//public async Task GetAllAsyncShouldReturnCorrectWithValidUserId()
		//{
		//	Guid userId = Guid.Parse("2A0209ED-267E-466A-AD5A-C611B2123418");

		//	string? expectedResult = "FC3B5A3B-AED6-4A94-9809-0AAAEC46AB44";

		//	IEnumerable<OrderViewModel> result = await this.orderService.GetAllAsync(userId);

		//	string? actualResult = result.Select(r => r.Id).ToString();

		//	Assert.That(expectedResult == actualResult);
		//}

		[Test]
		public async Task GetCraftersAsyncShouldReturnCorrectAllCrafters()
		{
			string expectedCrafterId = CrafterUser.Id.ToString();
			string expectedCrafterName = CrafterUser.UserName;

			IEnumerable<CrafterViewModel> result = await this.orderService.GetCraftersAsync();

			string actualCrafterId = result.ElementAt(0).Id.ToString();
			string actualCrafterName = result.ElementAt(0).Username;

			Assert.That(expectedCrafterId == actualCrafterId);
			Assert.That(expectedCrafterName == actualCrafterName);
		}


		//Order = new OrderViewModel()
		//{
		//	Id = "FC3B5A3B-AED6-4A94-9809-0AAAEC46AB44",
		//		Amount = 15.00m,
		//		Status = 3,
		//		Product = new ProductDetailsViewModel()
		//		{
		//			Id = 6,
		//			Type = "Hat",
		//			Description = "A banner with pink-white flags and brown string.",
		//			Price = 15.00m,
		//			ImagePath = "/img/Banner.jpg",
		//			IsCustom = false,
		//			Crafter = CrafterUser.UserName,
		//			CreatedOn = DateTime.Parse("2023-08-10 14:01:58.6363070")
		//		},
		//		CreatedOn = DateTime.Parse("2023-08-10 14:01:58.6363070")
		//	};
	}
}
