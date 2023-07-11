using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Purchase;

namespace CraftBuddy.Data.Models
{
	public class Purchase
	{
        public Purchase()
        {
			this.CreatedOn = DateTime.UtcNow;
		}

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        [Required]
        public ApplicationUser Client { get; set; } = null!;

        [Required]
		[MaxLength(AddressMaxLength)]
		public string DeliveryAddress { get; set; } = null!;

		[Required]
		public string ClientPhoneNumber { get; set; } = null!;

		[Required]
		public int ProductId { get; set; }

		[Required]
		public Product Product { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }
    }
}
