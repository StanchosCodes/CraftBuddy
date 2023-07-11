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
			this.Purchases = new HashSet<Purchase>();
			this.CustomOrders = new HashSet<CustomOrder>();
			this.Products = new HashSet<Product>();
			this.Events = new HashSet<Event>();
			this.Sets = new HashSet<Set>();
        }

        public bool IsDeleted { get; set; }

        public bool IsCrafter { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

		public virtual ICollection<CustomOrder> CustomOrders { get; set; }

		public virtual ICollection<Product> Products { get; set; }

		public virtual ICollection<Event> Events { get; set; }

		public virtual ICollection<Set> Sets { get; set; }
	}
}
