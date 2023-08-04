﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Order;
using static CraftBuddy.Common.OrderStatusConstants;

namespace CraftBuddy.Data.Models
{
    public class Order
	{
        public Order()
        {
			this.CreatedOn = DateTime.UtcNow;
            this.Products = new HashSet<ProductOrder>();
            this.StatusId = Waiting;
		}

        [Key]
        public int Id { get; set; }
        // MAKE GUID!

        public decimal? Amount { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        [Required]
        public ApplicationUser Client { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = null!;

        [Required]
		[MaxLength(AddressMaxLength)]
		public string DeliveryAddress { get; set; } = null!;

		[Required]
		public string ClientPhoneNumber { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

        public virtual ICollection<ProductOrder> Products { get; set; }
    }
}
