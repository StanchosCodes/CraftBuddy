using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Set;

namespace CraftBuddy.Data.Models
{
	public class Set
	{
        public Set()
        {
            this.Products = new HashSet<Product>();
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
		}

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImagePath { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }

        [Required]
        [ForeignKey(nameof(Maker))]
        public Guid MakerId { get; set; }

        [Required]
        public ApplicationUser Maker { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
