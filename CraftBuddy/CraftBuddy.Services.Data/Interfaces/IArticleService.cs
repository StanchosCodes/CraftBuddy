using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.Article;

namespace CraftBuddy.Services.Data.Interfaces
{
	public interface IArticleService
	{
		Task<IEnumerable<ArticleViewModel>> GetAllAsync();

		Task AddAsync(Guid userId, AddEditArticleViewModel articalModel);

		Task<ArticleDetailsViewModel> GetDetailsAsync(int id);

		Task<Article> GetArticleAsync(int id);

		Task EditAsync(Article articleToEdit, AddEditArticleViewModel editedArticle);

		Task DeleteAsync(Article articleToDelete);

		Task<bool> IsLikedAsync(Guid userId, int articleId);

		Task LikeAsync(Guid userId, int articleId);

		Task DislikeAsync(Guid userId, int articleId);
	}
}
