using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using CraftBuddy.Web.ViewModels.Article;
using CraftBuddy.Services.Data.Interfaces;

namespace CraftBuddy.Services.Data
{
	public class ArticleService : IArticleService
	{
		private readonly CraftBuddyDbContext context;

        public ArticleService(CraftBuddyDbContext context)
        {
			this.context = context;
        }

		public async Task<IEnumerable<ArticleViewModel>> GetAllAsync()
		{
			IEnumerable<ArticleViewModel> articles = await this.context
				.Articles
				.Where(a => a.IsDeleted == false)
				.Select(a => new ArticleViewModel()
				{
					Id = a.Id,
					Title = a.Title,
					Crafter = a.Crafter.UserName,
					LikesCount = a.LikesCount,
					CreatedOn = a.CreatedOn.ToString("f")
				})
				.ToListAsync();

			articles = articles
				.OrderByDescending(a => a.CreatedOn)
				.ThenByDescending(a => a.Id);

			return articles;
		}

		public async Task AddAsync(Guid userId, AddEditArticleViewModel articleModel)
		{
			try
			{
				Article newArticle = new Article()
				{
					Title = articleModel.Title,
					Description = articleModel.Description,
					CrafterId = userId
				};

				await this.context.Articles.AddAsync(newArticle);
				await this.context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw new ArgumentException();
			}
		}

		public async Task<ArticleDetailsViewModel> GetDetailsAsync(int id)
		{
			ArticleDetailsViewModel? articleDetailsModel = await this.context
				.Articles
				.Where(a => a.Id == id && a.IsDeleted == false)
				.Select(a => new ArticleDetailsViewModel()
				{
					Id = a.Id,
					Title = a.Title,
					Description = a.Description,
					Crafter = a.Crafter.UserName,
					LikesCount = a.LikesCount,
					CreatedOn = a.CreatedOn.ToString("f")
				})
				.FirstOrDefaultAsync();

			return articleDetailsModel!;
		}

		public async Task<Article> GetArticleAsync(int id)
		{
			Article? article = await this.context
				.Articles
				.Where(a => a.Id == id && a.IsDeleted == false)
				.FirstOrDefaultAsync();

			return article!;
		}

		public async Task EditAsync(Article articleToEdit, AddEditArticleViewModel editedArticle)
		{
			articleToEdit.Title = editedArticle.Title;
			articleToEdit.Description = editedArticle.Description;

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Article articleToDelete)
		{
			articleToDelete.IsDeleted = true;

			await this.context.SaveChangesAsync();
		}

		public async Task<bool> IsLikedAsync(Guid userId, int articleId)
		{
			return await this.context
				.ArticlesApplicationUsers
				.AnyAsync(aau => aau.ApplicationUserId == userId && aau.ArticleId == articleId);
		}

		public async Task LikeAsync(Guid userId, int articleId)
		{
			ArticleApplicationUser articleApplicationUser = new ArticleApplicationUser()
			{
				ArticleId = articleId,
				ApplicationUserId = userId
			};

			await this.context.ArticlesApplicationUsers.AddAsync(articleApplicationUser);
			await this.context.SaveChangesAsync();
		}

		public async Task DislikeAsync(Guid userId, int articleId)
		{
			ArticleApplicationUser? articleToRemove = await this.context
				.ArticlesApplicationUsers
				.Where(aau => aau.ApplicationUserId == userId && aau.ArticleId == articleId)
				.FirstOrDefaultAsync();

			if (articleToRemove == null)
			{
				return;
			}

			this.context.ArticlesApplicationUsers.Remove(articleToRemove);
			await this.context.SaveChangesAsync();
		}
	}
}
