using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.CustomOrder;

namespace CraftBuddy.Data.Models
{
	public class CustomOrder
	{
        public CustomOrder()
        {
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(ProductType))]
		public int ProductTypeId { get; set; }

		[Required]
		public ProductType ProductType { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Status))]
		public int StatusId { get; set; }

		[Required]
		public OrderStatus Status { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

        public virtual ICollection<UserCustomOrder> UserCustomOrders { get; set; }
    }
}
