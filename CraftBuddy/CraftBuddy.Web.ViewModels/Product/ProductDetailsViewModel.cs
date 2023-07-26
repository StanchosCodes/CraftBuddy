namespace CraftBuddy.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImagePath { get; set; } = null!;

        public bool IsCustom { get; set; }

        public string Crafter { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}
