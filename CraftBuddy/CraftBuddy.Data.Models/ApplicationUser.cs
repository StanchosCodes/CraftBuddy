using Microsoft.AspNetCore.Identity;

namespace CraftBuddy.Data.Models
{
	public class ApplicationUser : IdentityUser<Guid>
	{
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.IsDeleted = false;
			this.Purchases = new HashSet<UserPurchase>();
			this.CustomOrders = new HashSet<UserCustomOrder>();
			this.Products = new HashSet<UserProduct>();
			this.Events = new HashSet<UserEvent>();
			this.Sets = new HashSet<Set>();
        }

        public bool IsDeleted { get; set; }

		public virtual ICollection<UserPurchase> Purchases { get; set; }

		public virtual ICollection<UserCustomOrder> CustomOrders { get; set; }

		public virtual ICollection<UserProduct> Products { get; set; }

		public virtual ICollection<UserEvent> Events { get; set; }

		public virtual ICollection<Set> Sets { get; set; }
	}
}
