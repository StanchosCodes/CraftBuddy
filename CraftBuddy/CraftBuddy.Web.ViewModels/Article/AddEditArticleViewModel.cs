using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.Article;

namespace CraftBuddy.Web.ViewModels.Article
{
	public class AddEditArticleViewModel
	{
		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;
    }
}
