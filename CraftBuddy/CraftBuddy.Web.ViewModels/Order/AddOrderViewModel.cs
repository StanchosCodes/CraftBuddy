using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.Order;

namespace CraftBuddy.Web.ViewModels.Order
{
    public class AddOrderViewModel
    {
        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string DeliveryAddress { get; set; } = null!;

        [Required]
        [Phone]
        public string ClientPhoneNumber { get; set; } = null!;
    }
}
