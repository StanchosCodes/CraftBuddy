using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.Product;
using CraftBuddy.Web.ViewModels.Product.Enums;

namespace CraftBuddy.Services.Data.Interfaces
{
	public interface IProductService
    {
		AllFilteredProductsViewModel GetSortedProducts(AllProductsQueryModel queryModel);

		Task<IEnumerable<ProductTypeViewModel>> GetProductTypesAsync();

        Task AddAsync(Guid CrafterId, AddEditProductViewModel addProductModel);

        Task<ProductDetailsViewModel> GetDetailsAsync(int productId);

        Task<Product> GetProductAsync(int id);

        Task EditAsync(Product productToEdit, AddEditProductViewModel editModel);

        Task DeleteAsync(Product productToDelete);
    }
}
