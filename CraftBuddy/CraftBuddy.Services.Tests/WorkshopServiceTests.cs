using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data;
using Microsoft.EntityFrameworkCore;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Workshop;
using static CraftBuddy.Services.Tests.DatabaseSeeder;

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
					Id = NewWorkshop.Id,
					Title = NewWorkshop.Title,
					StartDate = NewWorkshop.StartDate.ToString("f"),
					Organiser = NewWorkshop.Organiser.UserName,
					ParticipantsCount = NewWorkshop.ParticipantsCount,
					ImagePath = NewWorkshop.ImagePath,
					CreatedOn = NewWorkshop.CreatedOn.ToString("f")
				}
			};

			IEnumerable<WorkshopViewModel> actualResult = await this.workshopService.GetAllAsync();

			Assert.That(expectedResult.Count() == actualResult.Count());
		}

		[Test]
		public async Task GetAllAsyncShouldReturnEmptyListIfNoWorkshops()
		{
			this.context.Workshops.Remove(NewWorkshop);
			await this.context.SaveChangesAsync();
			int expectedResult = 0;

			IEnumerable<WorkshopViewModel> actualResult = await this.workshopService.GetAllAsync();

			Assert.That(expectedResult == actualResult.Count());
		}

		[Test]
		public async Task AddAsyncShouldAddCorrectlyWithValidUserIdAndValidModel()
		{
			Guid userId = CrafterUser.Id;

			AddEditWorkshopViewModel workshopModel = new AddEditWorkshopViewModel()
			{
				Title = "New Workshop for test",
				Description = "Test description",
				StartDate = DateTime.Parse("2023-08-16 16:36:00.0000000"),
				EndDate = DateTime.Parse("2023-08-17 16:36:00.0000000")
			};

			await this.workshopServiceB.AddAsync(userId, workshopModel);

			Assert.That(this.contextB.Workshops.Any());
		}

		[Test]
		public void AddAsyncShouldThrowWithValidUserIdAndinvalidModel()
		{
			Guid userId = CrafterUser.Id;

			Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await this.workshopServiceB.AddAsync(userId, null!);
			});
		}

		[Test]
		public async Task GetWorshopAsyncShouldReturnWorshopById()
		{
			int workshopId = NewWorkshop.Id;

			CraftBuddy.Data.Models.Workshop result = await this.workshopService.GetWorkshopAsync(workshopId);

			Assert.That(workshopId == result.Id);
		}

		[Test]
		public async Task GetWorshopAsyncShouldReturnNullIfIdDoesNotExist()
		{
			int workshopId = int.MaxValue;

			CraftBuddy.Data.Models.Workshop result = await this.workshopService.GetWorkshopAsync(workshopId);

			Assert.IsNull(result);
		}

		[Test]
		public async Task GetWorshopAsyncShouldReturnNullIfWorkshopIsDeleted()
		{
			int workshopId = NewWorkshop.Id;
			NewWorkshop.IsDeleted = true;
			await this.context.SaveChangesAsync();

			CraftBuddy.Data.Models.Workshop result = await this.workshopService.GetWorkshopAsync(workshopId);

			Assert.IsNull(result);
		}

		[Test]
		public async Task EditAsyncShouldEditCorrectlyWithValidData()
		{
			Workshop workshopToEdit = NewWorkshop;

			AddEditWorkshopViewModel editModel = new AddEditWorkshopViewModel()
			{
				Title = "New Workshop for test",
				Description = "Test description",
				StartDate = DateTime.Parse("2023-08-16 16:36:00.0000000"),
				EndDate = DateTime.Parse("2023-08-17 16:36:00.0000000")
			};

			await this.workshopServiceB.EditAsync(workshopToEdit, editModel);

			Assert.That(editModel.Title == workshopToEdit.Title);
			Assert.That(editModel.Description == workshopToEdit.Description);
			Assert.That(editModel.StartDate == workshopToEdit.StartDate);
			Assert.That(editModel.EndDate == workshopToEdit.EndDate);
		}

		[Test]
		public void EditAsyncShouldThrowNullReferenceExceptionWithInvalidModel()
		{
			Workshop workshopToEdit = NewWorkshop;

			Assert.ThrowsAsync<NullReferenceException>(async () =>
			{
				await this.workshopServiceB.EditAsync(workshopToEdit, null!);
			});
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnCorrectlyWithValidData()
		{
			int workshopId = NewWorkshop.Id;

			WorkshopDetailsViewModel expectedResult = new WorkshopDetailsViewModel()
			{
				Id = workshopId,
				Title = NewWorkshop.Title,
				Description = NewWorkshop.Description,
				StartDate = NewWorkshop.StartDate.ToString("f"),
				EndDate = NewWorkshop.EndDate.ToString("f"),
				Organiser = NewWorkshop.Organiser.UserName,
				ImagePath = NewWorkshop.ImagePath,
				ParticipantsCount = NewWorkshop.ParticipantsCount,
				CreatedOn = NewWorkshop.CreatedOn.ToString("f")
			};

			WorkshopDetailsViewModel actualResult = await this.workshopServiceB.GetDetailsAsync(workshopId);

			Assert.That(expectedResult.Title == actualResult.Title);
			Assert.That(expectedResult.Description == actualResult.Description);
			Assert.That(expectedResult.StartDate == actualResult.StartDate);
			Assert.That(expectedResult.EndDate == actualResult.EndDate);
			Assert.That(expectedResult.Organiser == actualResult.Organiser);
			Assert.That(expectedResult.ImagePath == actualResult.ImagePath);
			Assert.That(expectedResult.ParticipantsCount == actualResult.ParticipantsCount);
			Assert.That(expectedResult.CreatedOn == actualResult.CreatedOn);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnNullWithInvalidId()
		{
			int workshopId = int.MaxValue;

			WorkshopDetailsViewModel actualResult = await this.workshopServiceB.GetDetailsAsync(workshopId);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task DeleteAsyncShouldSetIsDeletedToTrue()
		{
			await this.workshopService.DeleteAsync(NewWorkshop);

			Assert.IsTrue(NewWorkshop.IsDeleted);
		}

		[Test]
		public async Task GetJoinedAsyncShouldReturnAllJoinedWorkshops()
		{
			Guid userId = ClientUser.Id;

			WorkshopParticipant workshopParticipant = new WorkshopParticipant()
			{
				WorkshopId = NewWorkshop.Id,
				ParticipantId = userId
			};

			await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
			await this.context.SaveChangesAsync();

			IEnumerable<WorkshopViewModel> expectedResult = new List<WorkshopViewModel>()
			{
				new WorkshopViewModel()
				{
					Id = NewWorkshop.Id,
					Title = NewWorkshop.Title,
					StartDate = NewWorkshop.StartDate.ToString("f"),
					Organiser = NewWorkshop.Organiser.UserName,
					ParticipantsCount = NewWorkshop.ParticipantsCount,
					ImagePath = NewWorkshop.ImagePath,
					CreatedOn = NewWorkshop.CreatedOn.ToString("f")
				}
			};

			IEnumerable<WorkshopViewModel> actualResult = await this.workshopService.GetJoinedAsync(userId);

			Assert.That(expectedResult.Count() == actualResult.Count());
			Assert.That(expectedResult.ElementAt(0).Id == actualResult.ElementAt(0).Id);
		}

		[Test]
		public async Task GetJoinedAsyncShouldReturnEmptyList()
		{
			Guid userId = Guid.NewGuid();

			int expectedResult = 0;

			IEnumerable<WorkshopViewModel> actualResult = await this.workshopService.GetJoinedAsync(userId);

			Assert.That(expectedResult == actualResult.Count());
		}

		[Test]
		public async Task IsJoinedAsyncShouldReturnTrueIfIsJoined()
		{
			Guid userId = ClientUser.Id;

			WorkshopParticipant workshopParticipant = new WorkshopParticipant()
			{
				WorkshopId = NewWorkshop.Id,
				ParticipantId = userId
			};

			await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
			await this.context.SaveChangesAsync();

			bool actualResult = await this.workshopService.IsJoinedAsync(userId, NewWorkshop.Id);

			Assert.IsTrue(actualResult);
		}

		[Test]
		public async Task IsJoinedAsyncShouldReturnFalseIfIsNotJoined()
		{
			Guid userId = ClientUser.Id;

			bool actualResult = await this.workshopService.IsJoinedAsync(userId, NewWorkshop.Id);

			Assert.IsFalse(actualResult);
		}

		[Test]
		public async Task JoinAsyncShouldAddInParticipantWorkshops()
		{
			Guid userId = ClientUser.Id;

			await this.workshopService.JoinAsync(userId, NewWorkshop.Id);

			Assert.That(this.context.WorkshopsParticipants.Any());
		}

		[Test]
		public async Task CreateWorkshopViewModelShouldReturnCorrectModel()
		{
			WorkshopViewModel expectedResult = new WorkshopViewModel()
			{
				Id = NewWorkshop.Id,
				Title = NewWorkshop.Title,
				StartDate = NewWorkshop.StartDate.ToString("f"),
				Organiser = NewWorkshop.Organiser.UserName,
				ParticipantsCount = NewWorkshop.ParticipantsCount,
				ImagePath = NewWorkshop.ImagePath
			};

			WorkshopViewModel actualResult = await this.workshopService.CreateWorkshopViewModel(NewWorkshop.Id);

			Assert.That(expectedResult.Id == actualResult.Id);
			Assert.That(expectedResult.Title == actualResult.Title);
			Assert.That(expectedResult.StartDate == actualResult.StartDate);
			Assert.That(expectedResult.Organiser == actualResult.Organiser);
			Assert.That(expectedResult.ParticipantsCount == actualResult.ParticipantsCount);
			Assert.That(expectedResult.ImagePath == actualResult.ImagePath);
		}

		[Test]
		public async Task CreateWorkshopViewModelShouldReturnNullIfInvalidId()
		{
			int invalidId = int.MaxValue;

			WorkshopViewModel actualResult = await this.workshopService.CreateWorkshopViewModel(invalidId);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task LeaveAsyncShouldRemoveFromWorkshopsParticipants()
		{
			Guid userId = ClientUser.Id;

			WorkshopParticipant workshopParticipant = new WorkshopParticipant()
			{
				WorkshopId = NewWorkshop.Id,
				ParticipantId = userId
			};

			await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
			await this.context.SaveChangesAsync();

			await this.workshopService.LeaveAsync(userId, NewWorkshop.Id);

			Assert.IsFalse(this.context.WorkshopsParticipants.Any());
		}

		[Test]
		public async Task LeaveAsyncShouldNotRemoveFromWorkshopsParticipantsIfInvalidUserId()
		{
			Guid userId = Guid.NewGuid();

			WorkshopParticipant workshopParticipant = new WorkshopParticipant()
			{
				WorkshopId = NewWorkshop.Id,
				ParticipantId = ClientUser.Id
			};

			await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
			await this.context.SaveChangesAsync();

			await this.workshopService.LeaveAsync(userId, NewWorkshop.Id);

			Assert.IsTrue(this.context.WorkshopsParticipants.Any());
		}

		[Test]
		public async Task LeaveAsyncShouldNotRemoveFromWorkshopsParticipantsIfInvalidWorkshopId()
		{
			Guid userId = ClientUser.Id;
			int invalidWorkshopId = int.MaxValue;

			WorkshopParticipant workshopParticipant = new WorkshopParticipant()
			{
				WorkshopId = NewWorkshop.Id,
				ParticipantId = userId
			};

			await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
			await this.context.SaveChangesAsync();

			await this.workshopService.LeaveAsync(userId, invalidWorkshopId);

			Assert.IsTrue(this.context.WorkshopsParticipants.Any());
		}

		[Test]
		public async Task LeaveAsyncShouldDecrementParticipantsCountWithValidData()
		{
			Guid userId = ClientUser.Id;

			WorkshopParticipant workshopParticipant = new WorkshopParticipant()
			{
				WorkshopId = NewWorkshop.Id,
				ParticipantId = userId
			};

			NewWorkshop.ParticipantsCount += 1;

			await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
			await this.context.SaveChangesAsync();

			await this.workshopService.LeaveAsync(userId, NewWorkshop.Id);

			Assert.That(NewWorkshop.ParticipantsCount == 0);
		}
	}
}
