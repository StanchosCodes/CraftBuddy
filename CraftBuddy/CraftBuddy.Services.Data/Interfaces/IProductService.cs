using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftBuddy.Services.Data.Interfaces
{
     public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<IEnumerable<ProductTypeViewModel>> GetProductTypesAsync();

        Task AddAsync(Guid CrafterId, AddEditProductViewModel addProductModel);

        Task<ProductDetailsViewModel> GetDetailsAsync(int productId);

        Task<Product> GetProductAsync(int id);

        Task EditAsync(Product productToEdit, AddEditProductViewModel editModel);

        Task DeleteAsync(Product productToDelete);
    }
}
