using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CraftBuddy.Data.Models
{
	public class ArticleApplicationUser
	{
		[Required]
		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }

		[Required]
		public ApplicationUser ApplicationUser { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Article))]
		public int ArticleId { get; set; }

		[Required]
		public Article Article { get; set; } = null!;
	}
}
