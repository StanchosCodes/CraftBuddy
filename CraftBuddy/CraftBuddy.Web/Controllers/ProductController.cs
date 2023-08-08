using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CraftBuddy.Web.Infrastructure.Extensions;

namespace CraftBuddy.Web.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductService productService;
		private readonly IOrderService orderService;

        public ProductController(IProductService productService, IOrderService orderService)
        {
			this.productService = productService;
			this.orderService = orderService;
        }

        [HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery]AllProductsQueryModel queryModel)
		{
			AllFilteredProductsViewModel sortedProducts = this.productService.GetSortedProducts(queryModel);
			IEnumerable<ProductTypeViewModel> productTypes = await this.productService.GetProductTypesAsync();
			IEnumerable<CrafterViewModel> crafters = await this.orderService.GetCraftersAsync();

			queryModel.Products = sortedProducts.Products;
			queryModel.TotalProducts = sortedProducts.TotalProducts;
			queryModel.Types = productTypes;
			queryModel.Crafters = crafters;

			return View(queryModel);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			if (!this.User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("Index", "Home");
			}

			IEnumerable<ProductTypeViewModel> productTypes = await this.productService.GetProductTypesAsync();

			AddEditProductViewModel addProductModel = new AddEditProductViewModel()
			{
				Types = productTypes
			};

			return View(addProductModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddEditProductViewModel addProductModel)
		{
			if (!ModelState.IsValid)
			{
				addProductModel.Types = await this.productService.GetProductTypesAsync();

				return View(addProductModel);
			}

            var currentUserId = this.User.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            try
			{
				await this.productService.AddAsync(userId, addProductModel);

				return RedirectToAction("All", "Product");
			}
			catch (Exception)
			{
				ModelState.AddModelError("", "Failed to add product!");

				return View(addProductModel);
			}
        }

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			ProductDetailsViewModel productDetails = await this.productService.GetDetailsAsync(id);

			if (productDetails == null)
			{
				return View("NotFound");
			}

			return View(productDetails);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
            if (!this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<ProductTypeViewModel> productTypes = await this.productService.GetProductTypesAsync();

			Product productToEdit = await this.productService.GetProductAsync(id);

			if (productToEdit == null)
			{
				return View("BadRequest");
			}

            var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            if (productToEdit.CrafterId != userId)
			{
				return View("Unauthorised");
			}

			AddEditProductViewModel editModel = new AddEditProductViewModel()
			{
				Description = productToEdit.Description,
				Price = productToEdit.Price ?? 0,
				TypeId = productToEdit.TypeId,
				Types = productTypes
			};

			return View(editModel);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AddEditProductViewModel editModel)
		{
			if (!ModelState.IsValid)
			{
				return View(editModel);
			}

            Product productToEdit = await this.productService.GetProductAsync(id);

            if (productToEdit == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            if (productToEdit.CrafterId != userId)
            {
                return View("Unauthorised");
            }

			await this.productService.EditAsync(productToEdit, editModel);

			return RedirectToAction("All", "Product");
        }

		public async Task<IActionResult> Delete(int id)
		{
            Product productToDelete = await this.productService.GetProductAsync(id);

            if (productToDelete == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User.GetId();
            Guid userId = Guid.Parse(currentUserId!);

            if (productToDelete.CrafterId != userId)
            {
                return View("Unauthorised");
            }

			await this.productService.DeleteAsync(productToDelete);

			return RedirectToAction("All", "Product");
        }
	}
}
