namespace CraftBuddy.Web.ViewModels.Product
{
	public class AllFilteredProductsViewModel
	{
        public AllFilteredProductsViewModel()
        {
            this.Products = new HashSet<ProductViewModel>();
        }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
