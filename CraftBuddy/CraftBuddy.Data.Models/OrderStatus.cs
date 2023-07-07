using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.OrderStatus;

namespace CraftBuddy.Data.Models
{
	public class OrderStatus
	{
        public OrderStatus()
        {
            this.CustomOrders = new HashSet<CustomOrder>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<CustomOrder> CustomOrders { get; set; }
    }
}
