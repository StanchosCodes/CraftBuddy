using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.ProductType;

namespace CraftBuddy.Data.Models
{
	public class ProductType
	{
        public ProductType()
        {
            this.Products = new HashSet<Product>();
		}

        [Key]
        public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
