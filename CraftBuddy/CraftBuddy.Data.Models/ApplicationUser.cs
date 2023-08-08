using Microsoft.AspNetCore.Identity;

namespace CraftBuddy.Data.Models
{
	public class ApplicationUser : IdentityUser<Guid>
	{
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.IsDeleted = false;
			this.IsCrafter = false;
			this.Orders = new HashSet<Order>();
			this.Products = new HashSet<Product>();
			this.Workshops = new HashSet<Workshop>();
			this.JoinedWorkshops = new HashSet<WorkshopParticipant>();
			this.Articles = new HashSet<Article>();
			this.LikedArticles = new HashSet<ArticleApplicationUser>();
        }

        public bool IsDeleted { get; set; }

        public bool IsCrafter { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

		public virtual ICollection<Product> Products { get; set; }

		public virtual ICollection<Workshop> Workshops { get; set; }

		public virtual ICollection<WorkshopParticipant> JoinedWorkshops { get; set; }

		public virtual ICollection<Article> Articles { get; set; }

		public virtual ICollection<ArticleApplicationUser> LikedArticles { get; set; }
	}
}
