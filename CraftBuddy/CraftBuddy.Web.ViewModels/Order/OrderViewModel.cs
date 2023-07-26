using CraftBuddy.Web.ViewModels.Product;

namespace CraftBuddy.Web.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public decimal? Amount { get; set; }

        public string Status { get; set; } = null!;

        public ProductDetailsViewModel Product { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
    }
}
