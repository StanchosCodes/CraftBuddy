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
			this.Orders = new HashSet<ProductOrder>();
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
			this.IsCustom = false;
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

		public decimal? Price { get; set; }

		[Required]
		public string ImagePath { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Crafter))]
        public Guid CrafterId { get; set; }

		[Required]
		public ApplicationUser Crafter { get; set; } = null!;

        [Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

		[Required]
        public bool IsCustom { get; set; }

        public virtual ICollection<ProductOrder> Orders { get; set; }
    }
}
