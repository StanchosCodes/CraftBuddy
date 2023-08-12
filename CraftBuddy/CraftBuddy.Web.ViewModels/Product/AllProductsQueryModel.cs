using System.ComponentModel;
using CraftBuddy.Web.ViewModels.Order;
using System.ComponentModel.DataAnnotations;
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

		[DisplayName("Product")]
		public int TypeId { get; set; }

        [DisplayName("Crafter")]
		public Guid CrafterId { get; set; }

        [DisplayName("Sort products by:")]
		public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        [DisplayName("Products per page:")]
        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public virtual IEnumerable<ProductTypeViewModel> Types { get; set; }

        public virtual IEnumerable<CrafterViewModel> Crafters { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }
    }
}
