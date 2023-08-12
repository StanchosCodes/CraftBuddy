using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data;
using Microsoft.EntityFrameworkCore;
using CraftBuddy.Web.ViewModels.Article;
using CraftBuddy.Services.Data.Interfaces;
using static CraftBuddy.Services.Tests.DatabaseSeeder;

namespace CraftBuddy.Services.Tests
{
	public class ArticleServiceTests
	{
		private DbContextOptions<CraftBuddyDbContext> contextOptions;
		private CraftBuddyDbContext context;
		private CraftBuddyDbContext contextB;
		private IArticleService articleService;
		private IArticleService articleServiceB;

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

			this.articleService = new ArticleService(this.context);
			this.articleServiceB = new ArticleService(this.contextB);
		}

		[Test]
		public async Task GetAllAsyncShouldReturnAllArticles()
		{
			int expectedArticleId = NewArticle.Id;

			IEnumerable<ArticleViewModel> actualResult = await this.articleService.GetAllAsync();

			Assert.That(expectedArticleId == actualResult.ElementAt(0).Id);
		}

		[Test]
		public async Task GetAllAsyncShouldReturnEmptyListIfNoArticles()
		{
			this.context.Articles.Remove(NewArticle);
			await this.context.SaveChangesAsync();

			int expectedResult = 0;

			IEnumerable<ArticleViewModel> actualResult = await this.articleService.GetAllAsync();

			Assert.That(expectedResult == actualResult.Count());
		}

		[Test]
		public async Task AddAsyncShouldAddCorrectlyWithValidData()
		{
			Guid userId = CrafterUser.Id;

			AddEditArticleViewModel model = new AddEditArticleViewModel()
			{
				Title = "Funny article",
				Description = "A funny article to boost your mind."
			};

			await this.articleServiceB.AddAsync(userId, model);

			Assert.IsTrue(this.contextB.Articles.Any());
		}

		[Test]
		public void AddAsyncShouldThrowWithInvalidModel()
		{
			Guid userId = CrafterUser.Id;

			Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await this.articleServiceB.AddAsync(userId, null!);
			});
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnCorrectArticleDetailsViewModel()
		{
			ArticleDetailsViewModel expectedResult = new ArticleDetailsViewModel()
			{
				Id = NewArticle.Id,
				Title = NewArticle.Title,
				Description = NewArticle.Description,
				Crafter = NewArticle.Crafter.UserName,
				LikesCount = NewArticle.LikesCount,
				CreatedOn = NewArticle.CreatedOn.ToString("f")
			};

			ArticleDetailsViewModel actualResult = await this.articleService.GetDetailsAsync(NewArticle.Id);

			Assert.That(expectedResult.Id == actualResult.Id);
			Assert.That(expectedResult.Title == actualResult.Title);
			Assert.That(expectedResult.Description == actualResult.Description);
			Assert.That(expectedResult.Crafter == actualResult.Crafter);
			Assert.That(expectedResult.LikesCount == actualResult.LikesCount);
			Assert.That(expectedResult.CreatedOn == actualResult.CreatedOn);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnNullIfInvalidId()
		{
			int invalidId = int.MaxValue;

			ArticleDetailsViewModel actualResult = await this.articleService.GetDetailsAsync(invalidId);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnNullIfArticleIsDeleted()
		{
			NewArticle.IsDeleted = true;
			await this.context.SaveChangesAsync();

			ArticleDetailsViewModel actualResult = await this.articleService.GetDetailsAsync(NewArticle.Id);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task GetArticleAsyncShouldReturnArticleById()
		{
			Article actualResult = await this.articleService.GetArticleAsync(NewArticle.Id);

			Assert.That(NewArticle.Id == actualResult.Id);
		}

		[Test]
		public async Task GetArticleAsyncShouldReturnNullIfInvalidId()
		{
			int invalidId = int.MaxValue;

			Article actualResult = await this.articleService.GetArticleAsync(invalidId);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task GetArticleAsyncShouldReturnNullIfArticleIsDeleted()
		{
			NewArticle.IsDeleted = true;
			await this.context.SaveChangesAsync();

			Article actualResult = await this.articleService.GetArticleAsync(NewArticle.Id);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task EditAsyncShouldEditCorrectly()
		{
			AddEditArticleViewModel model = new AddEditArticleViewModel()
			{
				Title = "This is my interesting article",
				Description = "This article will be the most interesting topic to read.",
			};

			await this.articleService.EditAsync(NewArticle, model);

			Assert.That(model.Title == NewArticle.Title);
			Assert.That(model.Description == NewArticle.Description);
		}

		[Test]
		public void EditAsyncShouldThrowWithInvalidModel()
		{
			Assert.ThrowsAsync<NullReferenceException>(async () =>
			{
				await this.articleService.EditAsync(NewArticle, null!);
			});
		}

		[Test]
		public async Task DeleteAsyncShouldSetIsDeletedToTrue()
		{
			await this.articleService.DeleteAsync(NewArticle);

			Assert.IsTrue(NewArticle.IsDeleted);
		}

		[Test]
		public async Task IsLikedAsyncShouldReturnTrueIfItIsLiked()
		{
			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = NewArticle.Id,
				ApplicationUserId = ClientUser.Id
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();

			bool result = await this.articleService.IsLikedAsync(ClientUser.Id, NewArticle.Id);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsLikedAsyncShouldReturnFalseIfItIsNotLiked()
		{
			bool result = await this.articleService.IsLikedAsync(ClientUser.Id, NewArticle.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsLikedAsyncShouldReturnFalseIfInvalidUserId()
		{
			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = NewArticle.Id,
				ApplicationUserId = ClientUser.Id
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();

			bool result = await this.articleService.IsLikedAsync(CrafterUser.Id, NewArticle.Id);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task IsLikedAsyncShouldReturnFalseIfInvalidArticleId()
		{
			int invalidId = int.MaxValue;

			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = NewArticle.Id,
				ApplicationUserId = ClientUser.Id
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();

			bool result = await this.articleService.IsLikedAsync(ClientUser.Id, invalidId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task LikeAsyncShouldAddToArticlesApplicationUsers()
		{
			await this.articleService.LikeAsync(CrafterUser.Id, NewArticle.Id);

			Assert.IsTrue(this.context.ArticlesApplicationUsers.Any());
		}

		[Test]
		public async Task DislikeAsyncShouldRemoveFromArticlesApplicationUsers()
		{
			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = NewArticle.Id,
				ApplicationUserId = ClientUser.Id
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();

			await this.articleService.DislikeAsync(ClientUser.Id, NewArticle.Id);

			Assert.IsFalse(this.context.ArticlesApplicationUsers.Any());
		}

		[Test]
		public async Task DislikeAsyncShouldNotRemoveFromArticlesApplicationUsersIfInvalidUserId()
		{
			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = NewArticle.Id,
				ApplicationUserId = ClientUser.Id
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();

			await this.articleService.DislikeAsync(CrafterUser.Id, NewArticle.Id);

			Assert.IsTrue(this.context.ArticlesApplicationUsers.Any());
		}

		[Test]
		public async Task DislikeAsyncShouldNotRemoveFromArticlesApplicationUsersIfInvalidArticleId()
		{
			int invalidId = int.MaxValue;

			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = NewArticle.Id,
				ApplicationUserId = ClientUser.Id
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();

			await this.articleService.DislikeAsync(CrafterUser.Id, invalidId);

			Assert.IsTrue(this.context.ArticlesApplicationUsers.Any());
		}
	}

}
