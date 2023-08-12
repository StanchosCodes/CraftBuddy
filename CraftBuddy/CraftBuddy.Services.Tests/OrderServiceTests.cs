using CraftBuddy.Data;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Services.Data;
using Microsoft.EntityFrameworkCore;
using static CraftBuddy.Services.Tests.DatabaseSeeder;
using CraftBuddy.Web.ViewModels.Product;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Data.Models;

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

			this.context.Database.EnsureDeleted();
			this.context.Database.EnsureCreated();

			SeedDatabase(this.context);

			this.orderService = new OrderService(this.context);
			this.orderServiceB = new OrderService(this.contextB);
		}

		[Test]
		public async Task GetAllAsyncShouldReturnCorrectWithValidUserId()
		{
			Guid clientId = ClientUser.Id;

			string? expectedResult = DatabaseSeeder.Order.Id.ToString();

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllAsync(clientId);

			string? actualResult = result.ElementAt(0).Id.ToString();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllAsyncShouldReturnEmptyListWithInvalidUserId()
		{
			Guid clientId = Guid.NewGuid();

			int expectedResult = 0;

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllAsync(clientId);

			int actualResult = result.Count();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllAsyncShouldReturnEmptyListIfNoOrders()
		{
			Guid clientId = ClientUser.Id;

			int expectedResult = 0;

			this.context.Orders.Remove(DatabaseSeeder.Order);
			await this.context.SaveChangesAsync();

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllAsync(clientId);

			int actualResult = result.Count();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllWaitingAsyncShouldReturnAllWithStatusWaitingWithValidUserId()
		{
			HatProduct.IsCustom = true;

			DatabaseSeeder.Order.StatusId = 1; // Waiting

			await this.context.ProductsOrders.AddAsync(new ProductOrder()
				  {
				  	OrderId = DatabaseSeeder.Order.Id,
				  	ProductId = HatProduct.Id
				  });

			await this.context.SaveChangesAsync();

			Guid userId = HatProduct.CrafterId;

			string? expectedResult = DatabaseSeeder.Order.Id.ToString();

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllWaitingAsync(userId);

			string? actualResult = result.ElementAt(0).Id.ToString();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllWaitingAsyncShouldReturnAllWithStatusCraftingWithValidUserId()
		{
			HatProduct.IsCustom = true;

			DatabaseSeeder.Order.StatusId = 2; // Crafting

			await this.context.ProductsOrders.AddAsync(new ProductOrder()
			{
				OrderId = DatabaseSeeder.Order.Id,
				ProductId = HatProduct.Id
			});

			await this.context.SaveChangesAsync();

			Guid userId = HatProduct.CrafterId;

			string? expectedResult = DatabaseSeeder.Order.Id.ToString();

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllWaitingAsync(userId);

			string? actualResult = result.ElementAt(0).Id.ToString();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllWaitingAsyncShouldReturnEmptyListWithInvalidUserId()
		{
			Guid userId = Guid.NewGuid();

			int expectedResult = 0;

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllWaitingAsync(userId);

			int actualResult = result.Count();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllCraftedAsyncShouldReturnAllWithStatusCraftedWithValidUserId()
		{
			HatProduct.IsCustom = true;

			await this.context.ProductsOrders.AddAsync(new ProductOrder()
			{
				OrderId = DatabaseSeeder.Order.Id,
				ProductId = HatProduct.Id
			});

			await this.context.SaveChangesAsync();

			Guid userId = HatProduct.CrafterId;

			string? expectedResult = DatabaseSeeder.Order.Id.ToString();

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllCraftedAsync(userId);

			string? actualResult = result.ElementAt(0).Id.ToString();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task GetAllCraftedAsyncShouldReturnEmptyListWithInvalidUserId()
		{
			Guid userId = Guid.NewGuid();

			int expectedResult = 0;

			IEnumerable<OrderViewModel> result = await this.orderService.GetAllCraftedAsync(userId);

			int actualResult = result.Count();

			Assert.That(expectedResult == actualResult);
		}

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

		[Test]
		public async Task GetCraftersAsyncShouldReturnEmptyIfNoCrafters()
		{
			CrafterUser.IsDeleted = true;
			ClientUser.IsDeleted = true;

			await this.context.SaveChangesAsync();

			int expectedResult = 0;

			IEnumerable<CrafterViewModel> result = await this.orderService.GetCraftersAsync();

			int actualResult = result.Count();

			Assert.That(expectedResult == actualResult);
		}

		[Test]
		public async Task AddCustomAsyncShouldAddToProductsToOrdersAndProductsOrdersIfValidClientIdAndModel()
		{
			Guid clientId = ClientUser.Id;

			AddEditCustomOrderViewModel addModel = new AddEditCustomOrderViewModel()
			{
				DeliveryAddress = "SomeAdress",
				ClientPhoneNumber = "0888888888",
				ProductTypeId = 1,
				Description = "SomeDescriptionForTesting",
				CrafterId = CrafterUser.Id
			};

			await this.orderServiceB.AddCustomAsync(clientId, addModel);

			Assert.That(this.contextB.Products.Any());
			Assert.That(this.contextB.Orders.Any());
			Assert.That(this.contextB.ProductsOrders.Any());
		}

		[Test]
		public void AddCustomAsyncShouldThrowIfInvalidModel()
		{
			Guid clientId = ClientUser.Id;

			Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await this.orderServiceB.AddCustomAsync(clientId, null!);
			});
		}

		[Test]
		public async Task AddAsyncShouldAddToOrdersAndProductsOrdersIfValidUserIdClientIdAndModel()
		{
			Guid clientId = ClientUser.Id;
			int productId = HatProduct.Id;

			AddOrderViewModel model = new AddOrderViewModel()
			{
				ClientPhoneNumber = "0899999999",
				DeliveryAddress = "SomeDeliveryAddress"
			};

			await this.orderServiceB.AddAsync(clientId, productId, model);

			Assert.That(this.contextB.Orders.Any());
			Assert.That(this.contextB.ProductsOrders.Any());
		}

		[Test]
		public void AddAsyncShouldThrowIfInvalidModel()
		{
			Guid clientId = ClientUser.Id;
			int productId = HatProduct.Id;

			Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await this.orderServiceB.AddAsync(clientId, productId, null!);
			});
		}

		[Test]
		public void AddAsyncShouldThrowIfProductDoesNotExist()
		{
			Guid clientId = ClientUser.Id;
			int productId = int.MaxValue;

			AddOrderViewModel model = new AddOrderViewModel()
			{
				ClientPhoneNumber = "0899999999",
				DeliveryAddress = "SomeDeliveryAddress"
			};

			Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await this.orderServiceB.AddAsync(clientId, productId, model);
			});
		}

		[Test]
		public async Task GetOrderStatusesAsyncShouldReturnCorrectList()
		{
			int expectedCount = this.context.OrderStatuses.Count();
			string expectedWaitingName = Waiting.Name;
			string expectedCraftingName = Crafting.Name;
			string expectedCraftedName = Crafted.Name;

			IEnumerable<OrderStatusViewModel> result = await this.orderService.GetOrderStatusesAsync();

			Assert.That(expectedCount == result.Count());
			Assert.That(expectedWaitingName == result.ElementAt(0).Name);
			Assert.That(expectedCraftingName == result.ElementAt(1).Name);
			Assert.That(expectedCraftedName == result.ElementAt(2).Name);
		}

		//[Test]
		//public async Task GetOrderStatusesAsyncShouldReturnEmptyIfNoOrderStatuses()
		//{
		//	this.contextB.OrderStatuses.RemoveRange(new OrderStatus[] { Waiting, Crafting, Crafted });
		//	await this.contextB.SaveChangesAsync();

		//	int expectedCount = 0;

		//	IEnumerable<OrderStatusViewModel> result = await this.orderServiceB.GetOrderStatusesAsync();

		//	Assert.That(expectedCount == result.Count());
		//}

		[Test]
		public async Task GetOrderAsyncShouldReturnCorrectOrderById()
		{
			CraftBuddy.Data.Models.Order expectedOrder = DatabaseSeeder.Order;

			CraftBuddy.Data.Models.Order result = await this.orderService.GetOrderAsync(expectedOrder.Id);

			Assert.That(expectedOrder.Id == result.Id);
			Assert.That(expectedOrder.Amount == result.Amount);
			Assert.That(expectedOrder.DeliveryAddress == result.DeliveryAddress);
			Assert.That(expectedOrder.ClientPhoneNumber == result.ClientPhoneNumber);
			Assert.That(expectedOrder.CreatedOn == result.CreatedOn);
			Assert.That(expectedOrder.ClientId == result.ClientId);
			Assert.That(expectedOrder.StatusId == result.StatusId);
		}

		[Test]
		public async Task GetOrderAsyncShouldReturnNullIfOrderIdDoesNotExist()
		{
			Guid invalidId = Guid.NewGuid();

			CraftBuddy.Data.Models.Order result = await this.orderService.GetOrderAsync(invalidId);

			Assert.IsNull(result);
		}

		[Test]
		public async Task GetProductOrderAsyncShouldReturnCorrectProductOrderById()
		{
			await this.context.ProductsOrders.AddAsync(new ProductOrder()
			{
				OrderId = DatabaseSeeder.Order.Id,
				ProductId = HatProduct.Id
			});

			await this.context.SaveChangesAsync();

			ProductOrder expectedProductOrder = this.context.ProductsOrders.First();

			ProductOrder result = await this.orderService.GetProductOrderAsync(DatabaseSeeder.Order.Id);

			Assert.That(expectedProductOrder.OrderId == result.OrderId);
			Assert.That(expectedProductOrder.ProductId == result.ProductId);
		}

		[Test]
		public async Task GetProductOrderAsyncShouldReturnNullIfOrderIdDoesNotExist()
		{
			Guid invalidId = Guid.NewGuid();

			ProductOrder result = await this.orderService.GetProductOrderAsync(invalidId);

			Assert.IsNull(result);
		}

		[Test]
		public async Task EditAsyncShouldEditCorrectlyWithValidModel()
		{
			AddEditCustomOrderViewModel editModel = new AddEditCustomOrderViewModel()
			{
				StatusId = 3,
				Amount = 20.00m
			};

			CraftBuddy.Data.Models.Order orderToEdit = DatabaseSeeder.Order;

			await this.orderServiceB.EditAsync(orderToEdit, editModel);

			Assert.That(orderToEdit.StatusId == editModel.StatusId);
			Assert.That(orderToEdit.Amount == editModel.Amount);
		}

		[Test]
		public void EditAsyncShouldThrowWithNullModel()
		{
			CraftBuddy.Data.Models.Order orderToEdit = DatabaseSeeder.Order;

			Assert.ThrowsAsync<NullReferenceException>(async () =>
			{
				await this.orderServiceB.EditAsync(orderToEdit, null!);
			});
		}

		[Test]
		public void EditAsyncShouldThrowWithNullOrder()
		{
			AddEditCustomOrderViewModel editModel = new AddEditCustomOrderViewModel()
			{
				StatusId = 3,
				Amount = 20.00m
			};

			Assert.ThrowsAsync<NullReferenceException>(async () =>
			{
				await this.orderServiceB.EditAsync(null!, editModel);
			});
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnCorrectlyWithValidData()
		{
			CraftBuddy.Data.Models.Order order = DatabaseSeeder.Order;
			Guid orderId = order.Id;
			Guid clientId = ClientUser.Id;
			string username = ClientUser.UserName;

			order.Status = Crafted;

			ProductDetailsViewModel productModel = new ProductDetailsViewModel()
			{
				Type = "Hat",
				Description = "SomeDescription",
				ImagePath = "/img/Hat.jpg",
				Crafter = CrafterUser.UserName
			};

			OrderDetailsViewModel expectedResult = new OrderDetailsViewModel()
			{
				Id = order.Id,
				Amount = order.Amount ?? 0,
				Status = order.Status.Name,
				DeliveryAddress = order.DeliveryAddress,
				ClientPhoneNumber = order.ClientPhoneNumber,
				CreatedOn = order.CreatedOn.ToString("f"),
				Type = productModel.Type,
				Description = productModel.Description,
				ImagePath = productModel.ImagePath,
				Crafter = productModel.Crafter
			};

			OrderDetailsViewModel actualResult = await this.orderService.GetDetailsAsync(orderId, clientId, username, productModel);

			Assert.That(expectedResult.Id == actualResult.Id);
			Assert.That(expectedResult.Amount == actualResult.Amount);
			Assert.That(expectedResult.Status == actualResult.Status);
			Assert.That(expectedResult.DeliveryAddress == actualResult.DeliveryAddress);
			Assert.That(expectedResult.ClientPhoneNumber == actualResult.ClientPhoneNumber);
			Assert.That(expectedResult.CreatedOn == actualResult.CreatedOn);
			Assert.That(expectedResult.Type == actualResult.Type);
			Assert.That(expectedResult.Description == actualResult.Description);
			Assert.That(expectedResult.ImagePath == actualResult.ImagePath);
			Assert.That(expectedResult.Crafter == actualResult.Crafter);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnNullWithInvalidOrderId()
		{
			Guid invalidOrderId = Guid.NewGuid();
			Guid clientId = ClientUser.Id;
			string username = ClientUser.UserName;

			ProductDetailsViewModel productModel = new ProductDetailsViewModel()
			{
				Type = "Hat",
				Description = "SomeDescription",
				ImagePath = "/img/Hat.jpg",
				Crafter = CrafterUser.UserName
			};

			OrderDetailsViewModel result = await this.orderService.GetDetailsAsync(invalidOrderId, clientId, username, productModel);

			Assert.IsNull(result);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnNullWithInvalidUserIdAndUsernameNotACrafter()
		{
			CraftBuddy.Data.Models.Order order = DatabaseSeeder.Order;
			Guid orderId = order.Id;
			Guid invalidClientId = Guid.NewGuid();
			string username = ClientUser.UserName;

			order.Status = Crafted;

			ProductDetailsViewModel productModel = new ProductDetailsViewModel()
			{
				Type = "Hat",
				Description = "SomeDescription",
				ImagePath = "/img/Hat.jpg",
				Crafter = CrafterUser.UserName
			};

			OrderDetailsViewModel result = await this.orderService.GetDetailsAsync(orderId, invalidClientId, username, productModel);

			Assert.IsNull(result);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnCorrectWithInvalidUserIdAndValidUsernameCrafter()
		{
			CraftBuddy.Data.Models.Order order = DatabaseSeeder.Order;
			Guid orderId = order.Id;
			Guid invalidClientId = Guid.NewGuid();
			string username = CrafterUser.UserName;

			order.Status = Crafted;

			ProductDetailsViewModel productModel = new ProductDetailsViewModel()
			{
				Type = "Hat",
				Description = "SomeDescription",
				ImagePath = "/img/Hat.jpg",
				Crafter = CrafterUser.UserName
			};

			OrderDetailsViewModel expectedResult = new OrderDetailsViewModel()
			{
				Id = order.Id,
				Amount = order.Amount ?? 0,
				Status = order.Status.Name,
				DeliveryAddress = order.DeliveryAddress,
				ClientPhoneNumber = order.ClientPhoneNumber,
				CreatedOn = order.CreatedOn.ToString("f"),
				Type = productModel.Type,
				Description = productModel.Description,
				ImagePath = productModel.ImagePath,
				Crafter = productModel.Crafter
			};

			OrderDetailsViewModel actualResult = await this.orderService.GetDetailsAsync(orderId, invalidClientId, username, productModel);

			Assert.That(expectedResult.Id == actualResult.Id);
			Assert.That(expectedResult.Amount == actualResult.Amount);
			Assert.That(expectedResult.Status == actualResult.Status);
			Assert.That(expectedResult.DeliveryAddress == actualResult.DeliveryAddress);
			Assert.That(expectedResult.ClientPhoneNumber == actualResult.ClientPhoneNumber);
			Assert.That(expectedResult.CreatedOn == actualResult.CreatedOn);
			Assert.That(expectedResult.Type == actualResult.Type);
			Assert.That(expectedResult.Description == actualResult.Description);
			Assert.That(expectedResult.ImagePath == actualResult.ImagePath);
			Assert.That(expectedResult.Crafter == actualResult.Crafter);
		}

		[Test]
		public void GetDetailsAsyncShouldReturnNullWithInvalidProductModel()
		{
			CraftBuddy.Data.Models.Order order = DatabaseSeeder.Order;
			Guid orderId = order.Id;
			Guid clientId = ClientUser.Id;
			string username = ClientUser.UserName;

			order.Status = Crafted;

			Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await this.orderService.GetDetailsAsync(orderId, clientId, username, null!);
			});
		}
	}
}
