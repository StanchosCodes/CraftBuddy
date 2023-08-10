namespace CraftBuddy.Web.ViewModels.Article
{
	public class ArticleViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string Crafter { get; set; } = null!;

		public int LikesCount { get; set; }

		public string CreatedOn { get; set; } = null!;
	}
}
