using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Article;

namespace CraftBuddy.Data.Models
{
	public class Article
	{
        public Article()
        {
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
			this.LikesCount = 0;
			this.UsersLikes = new HashSet<ArticleApplicationUser>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Crafter))]
        public Guid CrafterId { get; set; }

		[Required]
		public ApplicationUser Crafter { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public int LikesCount { get; set; }

        public virtual ICollection<ArticleApplicationUser> UsersLikes { get; set; }
    }
}
