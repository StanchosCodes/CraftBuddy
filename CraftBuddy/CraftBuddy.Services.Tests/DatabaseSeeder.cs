using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.Product;

namespace CraftBuddy.Services.Tests
{
	public static class DatabaseSeeder
	{
		public static ApplicationUser CrafterUser;
		public static ApplicationUser ClientUser;
		public static Product HatProduct;
		public static Product BannerProduct;
		public static Product TopperProduct;
		public static Product FlagProduct;
		public static ProductType HatType;
		public static ProductType BannerType;
		public static ProductType TopperType;
		public static ProductType FlagType;
		public static OrderStatus Waiting;
		public static OrderStatus Crafting;
		public static OrderStatus Crafted;
		public static Order Order;
		public static Workshop Workshop;

		public static void SeedDatabase(CraftBuddyDbContext context)
		{
			CrafterUser = new ApplicationUser()
			{
				UserName = "CrafterUser",
				NormalizedUserName = "CRAFTERUSER",
				Email = "CrafterUser@abv.bg",
				NormalizedEmail = "CRAFTERUSER@ABV.BG",
				EmailConfirmed = false,
				PasswordHash = "AQAAAAEAACcQAAAAEMTacu0GSix4sriOl5PcyhkOoFL1+ELHx1BpXQhIciWXbMKbL5OAc8BR0NRaXHeTIw==",
				SecurityStamp = "DROPSNJOONLCTO3Y7KBYPYCX26L6XEF5",
				ConcurrencyStamp = "123fa273-b323-46ff-b000-d3337bb9922a",
				PhoneNumber = "0891111111",
				PhoneNumberConfirmed = false,
				IsDeleted = false,
				IsCrafter = true,
				TwoFactorEnabled = false,
				LockoutEnabled = true,
				AccessFailedCount = 0
			};

			ClientUser = new ApplicationUser()
			{
				UserName = "ClientUser",
				NormalizedUserName = "CLIENTUSER",
				Email = "ClientUser@abv.bg",
				NormalizedEmail = "CLIENTUSER@ABV.BG",
				EmailConfirmed = false,
				PasswordHash = "AQAAAAEAACcQAAAAEDkFv7d0XShPFU2RagXNwBWYzUFBU4K7TbMgHdjuqz+aij9zwR+26A1mIGrGhvhhOg==",
				SecurityStamp = "ZSFOWXCIMA24SJWNM7FVOQQPPURPDCYV",
				ConcurrencyStamp = "7eb3f76a-16df-403f-8fae-1000ad70d2a4",
				PhoneNumberConfirmed = false,
				IsDeleted = false,
				IsCrafter = false,
				TwoFactorEnabled = false,
				LockoutEnabled = true,
				AccessFailedCount = 0
			};

			HatType = new ProductType()
			{
				Name = "Hat"
			};

			BannerType = new ProductType()
			{
				Name = "Banner"
			};

			TopperType = new ProductType()
			{
				Name = "Topper"
			};

			FlagType = new ProductType()
			{
				Name = "Flag"
			};

			Waiting = new OrderStatus()
			{
				Name = "Waiting"
			};

			Crafting = new OrderStatus()
			{
				Name = "Crafting"
			};

			Crafted = new OrderStatus()
			{
				Name = "Crafted"
			};

			HatProduct = new Product()
			{
				TypeId = HatType.Id,
				Type = HatType,
				Description = "Hat with a pink pompon and white board",
				Price = 10.00m,
				ImagePath = "/img/Hat.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:33:36.7330540"),
				IsDeleted = false,
				CrafterId = CrafterUser.Id,
				Crafter = CrafterUser,
				IsCustom = false
			};

			BannerProduct = new Product()
			{
				TypeId = 2,
				Description = "Banner with pink flags",
				Price = 15.00m,
				ImagePath = "/img/Banner.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:34:12.0266885"),
				IsDeleted = false,
				CrafterId = CrafterUser.Id,
				Crafter = CrafterUser,
				IsCustom = false
			};

			TopperProduct = new Product()
			{
				TypeId = 3,
				Description = "Topper with fluffy pompon",
				Price = 6.00m,
				ImagePath = "/img/Topper.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 14:00:07.5083195"),
				IsDeleted = false,
				CrafterId = CrafterUser.Id,
				Crafter = CrafterUser,
				IsCustom = false
			};

			FlagProduct = new Product()
			{
				TypeId = 4,
				Description = "Flag with custom text.",
				Price = 12.00m,
				ImagePath = "/img/Flag.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:58:32.9025690"),
				IsDeleted = false,
				CrafterId = CrafterUser.Id,
				Crafter = CrafterUser,
				IsCustom = false
			};

			Order = new Order()
			{
				Amount = 15.00m,
				DeliveryAddress = "Varna",
				ClientPhoneNumber = "0888111111",
				CreatedOn = DateTime.Parse("2023-08-10 13:41:25.2159745"),
				ClientId = ClientUser.Id,
				StatusId = 3
			};

			Workshop = new Workshop()
			{
				Title = "Test",
				Description = "Test test test",
				StartDate = DateTime.Parse("2023-08-13 16:35:00.0000000"),
				EndDate = DateTime.Parse("2023-08-14 16:35:00.0000000"),
				OrganiserId = CrafterUser.Id,
				Organiser = CrafterUser,
				ParticipantsCount = 0,
				ImagePath = "/img/Workshop.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:36:17.4469093"),
				IsDeleted = false
			};

			context.Users.Add(CrafterUser);
			context.Users.Add(ClientUser);
			context.Products.Add(HatProduct);
			context.Products.Add(BannerProduct);
			context.Products.Add(TopperProduct);
			context.Products.Add(FlagProduct);
			context.ProductTypes.Add(HatType);
			context.ProductTypes.Add(BannerType);
			context.ProductTypes.Add(TopperType);
			context.ProductTypes.Add(FlagType);
			context.OrderStatuses.Add(Waiting);
			context.OrderStatuses.Add(Crafting);
			context.OrderStatuses.Add(Crafted);
			context.Orders.Add(Order);
			context.Workshops.Add(Workshop);
			context.SaveChanges();
		}
	}
}
