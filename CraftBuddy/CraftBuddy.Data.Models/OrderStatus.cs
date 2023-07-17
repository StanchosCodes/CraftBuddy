using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.OrderStatus;

namespace CraftBuddy.Data.Models
{
	public class OrderStatus
	{
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
