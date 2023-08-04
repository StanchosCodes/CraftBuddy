using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftBuddy.Data.Models
{
    public class ProductOrder
    {
        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        [Required]
        public Order Order { get; set; } = null!;
    }
}
