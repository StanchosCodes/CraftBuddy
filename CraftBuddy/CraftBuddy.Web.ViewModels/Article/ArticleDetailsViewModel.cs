namespace CraftBuddy.Web.ViewModels.Article
{
	public class ArticleDetailsViewModel
	{
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Crafter { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public int LikesCount { get; set; }

        public bool IsCurentUserLiked { get; set; }
    }
}
