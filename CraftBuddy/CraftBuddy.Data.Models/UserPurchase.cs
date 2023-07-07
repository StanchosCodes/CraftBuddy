using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftBuddy.Data.Models
{
	public class UserPurchase
	{
		[Required]
		[ForeignKey(nameof(User))]
		public Guid UserId { get; set; }

		[Required]
		public ApplicationUser User { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Purchase))]
		public int PurchaseId { get; set; }

		[Required]
		public Purchase Purchase { get; set; } = null!;
    }
}
