using System.ComponentModel.DataAnnotations;

namespace CraftBuddy.Web.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = null!;

        public string DeliveryAddress { get; set; } = null!;

        public string ClientPhoneNumber { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImagePath { get; set; } = null!;

        public string Crafter { get; set; } = null!;
    }
}
