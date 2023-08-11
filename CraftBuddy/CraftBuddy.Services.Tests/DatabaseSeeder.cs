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
		public static Order Order;

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

			HatProduct = new Product()
			{
				Id = 1,
				TypeId = 1,
				Description = "Hat with a pink pompon and white board",
				Price = 10.00m,
				ImagePath = "/img/Hat.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:33:36.7330540"),
				IsDeleted = false,
				CrafterId = Guid.Parse("F8E943F9-DB1A-4DF7-A624-12BC66227974"),
				IsCustom = false
			};

			BannerProduct = new Product()
			{
				Id = 2,
				TypeId = 2,
				Description = "Banner with pink flags",
				Price = 15.00m,
				ImagePath = "/img/Banner.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:34:12.0266885"),
				IsDeleted = false,
				CrafterId = Guid.Parse("F8E943F9-DB1A-4DF7-A624-12BC66227974"),
				IsCustom = false
			};

			TopperProduct = new Product()
			{
				Id = 3,
				TypeId = 3,
				Description = "Topper with fluffy pompon",
				Price = 6.00m,
				ImagePath = "/img/Topper.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 14:00:07.5083195"),
				IsDeleted = false,
				CrafterId = Guid.Parse("F8E943F9-DB1A-4DF7-A624-12BC66227974"),
				IsCustom = false
			};

			FlagProduct = new Product()
			{
				Id = 4,
				TypeId = 4,
				Description = "Flag with custom text.",
				Price = 12.00m,
				ImagePath = "/img/Flag.jpg",
				CreatedOn = DateTime.Parse("2023-08-10 13:58:32.9025690"),
				IsDeleted = false,
				CrafterId = Guid.Parse("F8E943F9-DB1A-4DF7-A624-12BC66227974"),
				IsCustom = false
			};

			HatType = new ProductType()
			{
				Id = 1,
				Name = "Hat"
			};

			BannerType = new ProductType()
			{
				Id = 2,
				Name = "Banner"
			};

			TopperType = new ProductType()
			{
				Id = 3,
				Name = "Topper"
			};

			FlagType = new ProductType()
			{
				Id = 4,
				Name = "Flag"
			};

			Order = new Order()
			{
				Id = Guid.Parse("FC3B5A3B-AED6-4A94-9809-0AAAEC46AB44"),
				Amount = 15.00m,
				DeliveryAddress = "Varna",
				ClientPhoneNumber = "0888111111",
				CreatedOn = DateTime.Parse("2023-08-10 13:41:25.2159745"),
				ClientId = Guid.Parse("2A0209ED-267E-466A-AD5A-C611B2123418"),
				StatusId = 3
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
			context.Orders.Add(Order);
		}
	}
}
