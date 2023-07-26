using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using static CraftBuddy.Services.Data.Utilities.ServiceUtilities;

namespace CraftBuddy.Services.Data
{
	public class ProductService : IProductService
	{
        private readonly CraftBuddyDbContext context;

        public ProductService(CraftBuddyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            IEnumerable<ProductViewModel> products = await this.context
                .Products
                .Where(p => p.IsDeleted == false && p.IsCustom == false)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Type = p.Type.Name,
                    Crafter = p.Crafter.UserName,
                    Price = p.Price ?? 0,
                    ImagePath = p.ImagePath
                })
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<ProductTypeViewModel>> GetProductTypesAsync()
        {
            IEnumerable<ProductTypeViewModel> productTypes = await this.context
                .ProductTypes
                .Select(pt => new ProductTypeViewModel()
                {
                    Id = pt.Id,
                    Name = pt.Name
                })
                .ToListAsync();

            return productTypes;
        }

        public async Task AddAsync(Guid crafterId, AddEditProductViewModel addProductModel)
        {
            try
            {
                Product newProduct = new Product()
                {
                    TypeId = addProductModel.TypeId,
                    Description = addProductModel.Description,
                    Price = addProductModel.Price,
                    CrafterId = crafterId
                };

                newProduct.ImagePath = ChangeImagePath(addProductModel.TypeId);

                await this.context.Products.AddAsync(newProduct);
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<ProductDetailsViewModel> GetDetailsAsync(int productId)
        {
            ProductDetailsViewModel? detailsModel = await this.context
                .Products
                .Where(p => p.Id == productId && p.IsDeleted == false)
                .Select(p => new ProductDetailsViewModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Type = p.Type.Name,
                    Price = p.Price ?? 0,
                    ImagePath = p.ImagePath,
                    Crafter = p.Crafter.UserName,
                    CreatedOn = p.CreatedOn
                })
                .FirstOrDefaultAsync();

            return detailsModel!;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            Product? product = await this.context.Products.FindAsync(id);

            if (product?.IsDeleted == true)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }

#pragma warning disable CS8603 // Possible null reference return.
            return product;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task EditAsync(Product productToEdit, AddEditProductViewModel editModel)
        {
            productToEdit.Description = editModel.Description;
            productToEdit.Price = editModel.Price;
            productToEdit.TypeId = editModel.TypeId;
            productToEdit.ImagePath = ChangeImagePath(editModel.TypeId);

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product productToDelete)
        {
            productToDelete.IsDeleted = true;

            await this.context.SaveChangesAsync();
        }
    }
}