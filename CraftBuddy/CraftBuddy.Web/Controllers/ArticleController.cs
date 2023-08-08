using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.Infrastructure.Extensions;
using CraftBuddy.Web.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftBuddy.Web.Controllers
{
	[Authorize]
	public class ArticleController : Controller
	{
		private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
			this.articleService = articleService;
        }

		[HttpGet]
        public async Task<IActionResult> All()
		{
			IEnumerable<ArticleViewModel> articles = await this.articleService.GetAllAsync();

			return View(articles);
		}

		[HttpGet]
		public IActionResult Add()
		{
			AddEditArticleViewModel articleModel = new AddEditArticleViewModel();

			return View(articleModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddEditArticleViewModel articleModel)
		{
			if (!ModelState.IsValid)
			{
				return View(articleModel);
			}

			var currentUserId = this.User.GetId();
			Guid userId = Guid.Parse(currentUserId!);

			try
			{
				await this.articleService.AddAsync(userId, articleModel);

				return RedirectToAction("All", "Article");
			}
			catch (Exception)
			{
				ModelState.AddModelError("", "Failed to add article!");

				return View(articleModel);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Read(int id)
		{
			var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

			ArticleDetailsViewModel articleDetails = await this.articleService.GetDetailsAsync(id);

			if (articleDetails == null)
			{
				return View("NotFound");
			}

			bool isLiked = await this.articleService.IsLikedAsync(userId, id);

			articleDetails.IsCurentUserLiked = isLiked;

			return View(articleDetails);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (!this.User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("Index", "Home");
			}

			Article articleToEdit = await this.articleService.GetArticleAsync(id);

			if (articleToEdit == null)
			{
				return View("BadRequest");
			}

			var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

			if (articleToEdit.CrafterId != userId)
			{
				return View("Unauthorised");
			}

			AddEditArticleViewModel articleModel = new AddEditArticleViewModel()
			{
				Title = articleToEdit.Title,
				Description = articleToEdit.Description
			};

			return View(articleModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AddEditArticleViewModel editedArticle)
		{
			if (!ModelState.IsValid)
			{
				return View(editedArticle);
			}

			Article articleToEdit = await this.articleService.GetArticleAsync(id);

			if (articleToEdit == null)
			{
				return View("BadRequest");
			}

			var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

			if (articleToEdit.CrafterId != userId)
			{
				return View("Unauthorised");
			}

			await this.articleService.EditAsync(articleToEdit, editedArticle);

			return RedirectToAction("All", "Article");
		}

		public async Task<IActionResult> Like(int id)
		{
			Article articleToLike = await this.articleService.GetArticleAsync(id);

			if (articleToLike == null)
			{
				return View("BadRequest");
			}

			var currentUserId = this.User!.GetId();

			if (currentUserId == null)
			{
				return View("Unauthorised");
			}

			Guid userId = Guid.Parse(currentUserId!);

			bool isLiked = await this.articleService.IsLikedAsync(userId, id);

			if (!isLiked)
			{
				articleToLike.LikesCount += 1;

				await this.articleService.LikeAsync(userId, id);
			}

			return RedirectToAction("All", "Article");
		}

		public async Task<IActionResult> Dislike(int id)
		{
			Article articleToDislike = await this.articleService.GetArticleAsync(id);

			if (articleToDislike == null)
			{
				return View("BadRequest");
			}

			var currentUserId = this.User!.GetId();

			if (currentUserId == null)
			{
				return View("Unauthorised");
			}

			Guid userId = Guid.Parse(currentUserId!);

			bool isLiked = await this.articleService.IsLikedAsync(userId, id);

			if (isLiked)
			{
				articleToDislike.LikesCount -= 1;

				await this.articleService.DislikeAsync(userId, id);
			}

			return RedirectToAction("All", "Article");
		}

		public async Task<IActionResult> Delete(int id)
		{
			Article articleToDelete = await this.articleService.GetArticleAsync(id);

			if (articleToDelete == null)
			{
				return View("BadRequest");
			}

			var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

			if (articleToDelete.CrafterId != userId)
			{
				return View("Unauthorised");
			}

			await this.articleService.DeleteAsync(articleToDelete);

			return RedirectToAction("All", "Article");
		}
	}
}
