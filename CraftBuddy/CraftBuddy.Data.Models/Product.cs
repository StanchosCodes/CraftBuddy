using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Product;

namespace CraftBuddy.Data.Models
{
	public class Product
	{
        public Product()
        {
			this.UserProducts = new HashSet<UserProduct>();
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
		}

        [Key]
        public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Type))]
		public int TypeId { get; set; }

		[Required]
		public ProductType Type { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string ImagePath { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

        public virtual ICollection<UserProduct> UserProducts { get; set; }
    }
}
