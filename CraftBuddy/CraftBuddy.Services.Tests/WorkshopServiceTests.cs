using CraftBuddy.Data;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Services.Data;
using Microsoft.EntityFrameworkCore;
using static CraftBuddy.Services.Tests.DatabaseSeeder;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Workshop;

namespace CraftBuddy.Services.Tests
{
	public class WorkshopServiceTests
	{
		private DbContextOptions<CraftBuddyDbContext> contextOptions;
		private CraftBuddyDbContext context;
		private CraftBuddyDbContext contextB;
		private IWorkshopService workshopService;
		private IWorkshopService workshopServiceB;

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

			this.workshopService = new WorkshopService(this.context);
			this.workshopServiceB = new WorkshopService(this.contextB);
		}

		[Test]
		public async Task GetAllAsyncShouldReturnAllWorkshops()
		{
			IEnumerable<WorkshopViewModel> expectedResult = new List<WorkshopViewModel>()
			{
				new WorkshopViewModel()
				{
					Id = Workshop.Id,
					Title = Workshop.Title,
					StartDate = Workshop.StartDate.ToString("f"),
					Organiser = Workshop.Organiser.UserName,
					ParticipantsCount = Workshop.ParticipantsCount,
					ImagePath = Workshop.ImagePath,
					CreatedOn = Workshop.CreatedOn.ToString("f")
				}
			};

			IEnumerable<WorkshopViewModel> actualResult = await this.workshopService.GetAllAsync();

			Assert.That(expectedResult.Count() == actualResult.Count());
		}

		[Test]
		public async Task GetAllAsyncShouldReturnEmptyListIfNoWorkshops()
		{
			this.context.Workshops.Remove(Workshop);
			await this.context.SaveChangesAsync();
			int expectedResult = 0;

			IEnumerable<WorkshopViewModel> actualResult = await this.workshopService.GetAllAsync();

			Assert.That(expectedResult == actualResult.Count());
		}
	}
}
