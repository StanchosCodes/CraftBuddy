﻿using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using CraftBuddy.Web.ViewModels.Product;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Product.Enums;
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

		public AllFilteredProductsViewModel GetSortedProducts(AllProductsQueryModel queryModel)
		{
            var productsQuery = this.context
                .Products
                .Where(p => p.IsDeleted == false && p.IsCustom == false)
                .AsQueryable();

            if (queryModel.TypeId != 0)
            {
                productsQuery = productsQuery.Where(p => p.Type.Id == queryModel.TypeId);
            }

            if (queryModel.CrafterId != default)
            {
                productsQuery = productsQuery.Where(p => p.Crafter.Id == queryModel.CrafterId);
            }

            productsQuery = queryModel.Sorting switch
            {
                ProductSorting.PriceAscending => productsQuery.OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery.OrderByDescending(p => p.Price),
                ProductSorting.Oldest => productsQuery.OrderBy(p => p.CreatedOn),
                ProductSorting.Newest => productsQuery.OrderByDescending(p => p.CreatedOn),
                _ => productsQuery.OrderByDescending(p => p.CreatedOn)
            };

            IEnumerable<ProductViewModel> products = productsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Type = p.Type.Name,
                    Crafter = p.Crafter.UserName,
                    Price = p.Price ?? 0,
                    ImagePath = p.ImagePath
                })
                .ToList();

            int totalProductsCount = productsQuery.Count();

            AllFilteredProductsViewModel filteredProducts = new AllFilteredProductsViewModel()
            {
                TotalProducts = totalProductsCount,
                Products = products
            };

            return filteredProducts;
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

                bool isCustom = false;

                newProduct.ImagePath = ChangeImagePath(addProductModel.TypeId, isCustom);

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
                return null!;
            }

            return product!;
        }

        public async Task EditAsync(Product productToEdit, AddEditProductViewModel editModel)
        {
			bool isCustom = false;

			productToEdit.Description = editModel.Description;
            productToEdit.Price = editModel.Price;
            productToEdit.TypeId = editModel.TypeId;
            productToEdit.ImagePath = ChangeImagePath(editModel.TypeId, isCustom);

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product productToDelete)
        {
            productToDelete.IsDeleted = true;

            await this.context.SaveChangesAsync();
        }
	}
}