using CraftBuddy.Web.ViewModels.Product;
using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.Order;
using static CraftBuddy.Common.EntityValidationConstants.Product;

namespace CraftBuddy.Web.ViewModels.Order
{
    public class AddEditCustomOrderViewModel
    {
        public AddEditCustomOrderViewModel()
        {
            this.ProductTypes = new HashSet<ProductTypeViewModel>();
            this.Crafters = new HashSet<CrafterViewModel>();
            this.OrderStatuses = new HashSet<OrderStatusViewModel>();
        }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), AmountMinValue, AmountMaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string DeliveryAddress { get; set; } = null!;

        [Required]
        [Phone]
        public string ClientPhoneNumber { get; set; } = null!;

        [Required]
        public int StatusId { get; set; }

        [Required]
        public Guid CrafterId { get; set; }

        public int ProductTypeId { get; set; }

        public virtual IEnumerable<ProductTypeViewModel> ProductTypes { get; set; }

        public virtual IEnumerable<CrafterViewModel> Crafters { get; set; }

        public virtual IEnumerable<OrderStatusViewModel> OrderStatuses { get; set; }
    }
}
