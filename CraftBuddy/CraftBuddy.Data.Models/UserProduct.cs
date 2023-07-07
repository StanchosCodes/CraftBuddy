using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftBuddy.Data.Models
{
	public class UserProduct
	{
		[Required]
		[ForeignKey(nameof(Maker))]
		public Guid MakerId { get; set; }

		[Required]
		public ApplicationUser Maker { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }

		[Required]
		public Product Product { get; set; } = null!;
	}
}
