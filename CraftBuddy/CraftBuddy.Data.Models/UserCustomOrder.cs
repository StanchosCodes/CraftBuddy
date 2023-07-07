using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftBuddy.Data.Models
{
	public class UserCustomOrder
	{
		[Required]
		[ForeignKey(nameof(Client))]
		public Guid ClientId { get; set; }

		[Required]
		public ApplicationUser Client { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(CustomOrder))]
		public int CustomOrderId { get; set; }

		[Required]
		public CustomOrder CustomOrder { get; set; } = null!;
    }
}
