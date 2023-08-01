using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Product.Enums;

namespace CraftBuddy.Web.ViewModels.Product
{
	public class AllProductsQueryModel
	{
        public AllProductsQueryModel()
        {
            this.Types = new HashSet<ProductTypeViewModel>();
            this.Crafters = new HashSet<CrafterViewModel>();
            this.Products = new HashSet<ProductViewModel>();
            this.CurrentPage = 1;
            this.ProductsPerPage = 4;
        }

        public int TypeId { get; set; }

		public Guid CrafterId { get; set; }

		public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public virtual IEnumerable<ProductTypeViewModel> Types { get; set; }

        public virtual IEnumerable<CrafterViewModel> Crafters { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }
    }
}
