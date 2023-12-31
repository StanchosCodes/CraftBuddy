using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data;
using Microsoft.EntityFrameworkCore;
using CraftBuddy.Web.ViewModels.Product;
using CraftBuddy.Services.Data.Interfaces;
using static CraftBuddy.Services.Tests.DatabaseSeeder;
using static CraftBuddy.Web.ViewModels.Product.Enums.ProductSorting;

namespace CraftBuddy.Services.Tests
{
	public class ProductServiceTests
	{
		private DbContextOptions<CraftBuddyDbContext> contextOptions;
		private CraftBuddyDbContext context;
		private CraftBuddyDbContext contextB;
		private IProductService productService;
		private IProductService productServiceB;

		[SetUp]
		public void SetUp()
		{
			this.contextOptions = new DbContextOptionsBuilder<CraftBuddyDbContext>()
				.UseInMemoryDatabase("CraftBuddyInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.context = new CraftBuddyDbContext(this.contextOptions);
			this.contextB = new CraftBuddyDbContext(this.contextOptions);

			this.context.Database.EnsureDeleted();
			this.contextB.Database.EnsureDeleted();
			this.context.Database.EnsureCreated();
			this.contextB.Database.EnsureCreated();

			SeedDatabase(this.context);

			this.productService = new ProductService(this.context);
			this.productServiceB = new ProductService(this.contextB);
		}

		[Test]
		public async Task GetProductAsyncShouldReturnCorrectProductWithGivenId()
		{
			int productId = HatProduct.Id;

			Product resultProduct = await this.productService.GetProductAsync(productId);

			Assert.That(productId == resultProduct.Id);
		}

		[Test]
		public async Task GetProductAsyncShouldReturnNullIfProductDoesNotExist()
		{
			int notExistingProductId = 1000;

			Product resultProduct = await this.productService.GetProductAsync(notExistingProductId);

			Assert.IsNull(resultProduct);
		}

		[Test]
		public async Task GetProductAsyncShouldReturnNullIfIdIsNotValid()
		{
			int notExistingProductId = -1;

			Product resultProduct = await this.productService.GetProductAsync(notExistingProductId);

			Assert.IsNull(resultProduct);
		}

		[Test]
		public async Task GetProductAsyncShouldReturnNullIfProductIsDeleted()
		{
			Product expectedProduct = HatProduct;

			expectedProduct.IsDeleted = true;

			Product resultProduct = await this.productService.GetProductAsync(expectedProduct.Id);

			Assert.IsNull(resultProduct);
		}

		[Test]
		public void GetSortedProductsShouldReturnSortedByDateDescending()
		{
			AllFilteredProductsViewModel expectedResult = new AllFilteredProductsViewModel()
			{
				TotalProducts = 4,
				Products = new HashSet<ProductViewModel>()
				{
					new ProductViewModel()
					{
						Id = TopperProduct.Id
					},
					new ProductViewModel()
					{
						Id = FlagProduct.Id
					},
					new ProductViewModel()
					{
						Id = BannerProduct.Id
					},
					new ProductViewModel()
					{
						Id = HatProduct.Id
					}
				}
			};

			AllProductsQueryModel queryModel = new AllProductsQueryModel()
			{
				TypeId = 0,
				CrafterId = default,
				Sorting = Newest,
				ProductsPerPage = 4,
				CurrentPage = 1
			};

			AllFilteredProductsViewModel filteredProducts = this.productService.GetSortedProducts(queryModel);

			Assert.That(expectedResult.TotalProducts == filteredProducts.TotalProducts);
			Assert.That(expectedResult.Products.ElementAt(0).Id == filteredProducts.Products.ElementAt(0).Id);
			Assert.That(expectedResult.Products.ElementAt(1).Id == filteredProducts.Products.ElementAt(1).Id);
			Assert.That(expectedResult.Products.ElementAt(2).Id == filteredProducts.Products.ElementAt(2).Id);
			Assert.That(expectedResult.Products.ElementAt(3).Id == filteredProducts.Products.ElementAt(3).Id);
		}

		[Test]
		public void GetSortedProductsShouldReturnSortedByDateAscending()
		{
			AllFilteredProductsViewModel expectedResult = new AllFilteredProductsViewModel()
			{
				TotalProducts = 4,
				Products = new HashSet<ProductViewModel>()
				{
					new ProductViewModel()
					{
						Id = HatProduct.Id
					},
					new ProductViewModel()
					{
						Id = BannerProduct.Id
					},
					new ProductViewModel()
					{
						Id = FlagProduct.Id
					},
					new ProductViewModel()
					{
						Id = TopperProduct.Id
					}
				}
			};

			AllProductsQueryModel queryModel = new AllProductsQueryModel()
			{
				TypeId = 0,
				CrafterId = default,
				Sorting = Oldest,
				ProductsPerPage = 4,
				CurrentPage = 1
			};

			AllFilteredProductsViewModel filteredProducts = this.productService.GetSortedProducts(queryModel);

			Assert.That(expectedResult.TotalProducts == filteredProducts.TotalProducts);
			Assert.That(expectedResult.Products.ElementAt(0).Id == filteredProducts.Products.ElementAt(0).Id);
			Assert.That(expectedResult.Products.ElementAt(1).Id == filteredProducts.Products.ElementAt(1).Id);
			Assert.That(expectedResult.Products.ElementAt(2).Id == filteredProducts.Products.ElementAt(2).Id);
			Assert.That(expectedResult.Products.ElementAt(3).Id == filteredProducts.Products.ElementAt(3).Id);
		}

		[Test]
		public void GetSortedProductsShouldReturnSortedByPriceAscending()
		{
			AllFilteredProductsViewModel expectedResult = new AllFilteredProductsViewModel()
			{
				TotalProducts = 4,
				Products = new HashSet<ProductViewModel>()
				{
					new ProductViewModel()
					{
						Id = TopperProduct.Id
					},
					new ProductViewModel()
					{
						Id = HatProduct.Id
					},
					new ProductViewModel()
					{
						Id = FlagProduct.Id
					},
					new ProductViewModel()
					{
						Id = BannerProduct.Id
					}
				}
			};

			AllProductsQueryModel queryModel = new AllProductsQueryModel()
			{
				TypeId = 0,
				CrafterId = default,
				Sorting = PriceAscending,
				ProductsPerPage = 4,
				CurrentPage = 1
			};

			AllFilteredProductsViewModel filteredProducts = this.productService.GetSortedProducts(queryModel);

			Assert.That(expectedResult.TotalProducts == filteredProducts.TotalProducts);
			Assert.That(expectedResult.Products.ElementAt(0).Id == filteredProducts.Products.ElementAt(0).Id);
			Assert.That(expectedResult.Products.ElementAt(1).Id == filteredProducts.Products.ElementAt(1).Id);
			Assert.That(expectedResult.Products.ElementAt(2).Id == filteredProducts.Products.ElementAt(2).Id);
			Assert.That(expectedResult.Products.ElementAt(3).Id == filteredProducts.Products.ElementAt(3).Id);
		}

		[Test]
		public void GetSortedProductsShouldReturnSortedByPriceDescending()
		{
			AllFilteredProductsViewModel expectedResult = new AllFilteredProductsViewModel()
			{
				TotalProducts = 4,
				Products = new HashSet<ProductViewModel>()
				{
					new ProductViewModel()
					{
						Id = BannerProduct.Id
					},
					new ProductViewModel()
					{
						Id = FlagProduct.Id
					},
					new ProductViewModel()
					{
						Id = HatProduct.Id
					},
					new ProductViewModel()
					{
						Id = TopperProduct.Id
					}
				}
			};

			AllProductsQueryModel queryModel = new AllProductsQueryModel()
			{
				TypeId = 0,
				CrafterId = default,
				Sorting = PriceDescending,
				ProductsPerPage = 4,
				CurrentPage = 1
			};

			AllFilteredProductsViewModel filteredProducts = this.productService.GetSortedProducts(queryModel);

			Assert.That(expectedResult.TotalProducts == filteredProducts.TotalProducts);
			Assert.That(expectedResult.Products.ElementAt(0).Id == filteredProducts.Products.ElementAt(0).Id);
			Assert.That(expectedResult.Products.ElementAt(1).Id == filteredProducts.Products.ElementAt(1).Id);
			Assert.That(expectedResult.Products.ElementAt(2).Id == filteredProducts.Products.ElementAt(2).Id);
			Assert.That(expectedResult.Products.ElementAt(3).Id == filteredProducts.Products.ElementAt(3).Id);
		}

		[Test]
		public async Task GetProductTypesAsyncShouldReturnCorrectTypes()
		{
			IEnumerable<ProductTypeViewModel> expectedResult = new List<ProductTypeViewModel>()
			{
				new ProductTypeViewModel()
				{
					Id = 1,
					Name = HatType.Name
				},
				new ProductTypeViewModel()
				{
					Id = 2,
					Name = BannerType.Name
				},
				new ProductTypeViewModel()
				{
					Id = 3,
					Name = TopperType.Name
				},
				new ProductTypeViewModel()
				{
					Id = 4,
					Name = FlagType.Name
				}
			};

			IEnumerable<ProductTypeViewModel> actualResult = await this.productService.GetProductTypesAsync();

			Assert.That(expectedResult.Count() == actualResult.Count());
			Assert.That(expectedResult.ElementAt(0).Id == actualResult.ElementAt(0).Id);
			Assert.That(expectedResult.ElementAt(1).Id == actualResult.ElementAt(1).Id);
			Assert.That(expectedResult.ElementAt(2).Id == actualResult.ElementAt(2).Id);
			Assert.That(expectedResult.ElementAt(3).Id == actualResult.ElementAt(3).Id);
		}

		[Test]
		public async Task AddAsyncShouldAddToDbCorrectly()
		{
			AddEditProductViewModel addModel = new AddEditProductViewModel()
			{
				Description = "Very cute hat with a green pompon.",
				Price = 10.00m,
				TypeId = 1
			};

			var userId = CrafterUser.Id;

			await this.productServiceB.AddAsync(userId, addModel);

			int expectedProductId = 1;

			Assert.That(this.contextB.Products.Any(p => p.Id == expectedProductId));
		}

		[Test]
		public void AddAsyncShouldThrowIfAddProductModelIsNull()
		{
			var userId = Guid.NewGuid();

			Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await this.productServiceB.AddAsync(userId, null!);
			});
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnCorrectlyWithValidId()
		{
			int productId = HatProduct.Id;

			ProductDetailsViewModel expectedResult = new ProductDetailsViewModel()
			{
				Id = HatProduct.Id,
				Description = HatProduct.Description,
				Type = "Hat",
				Price = HatProduct.Price ?? 0,
				ImagePath = HatProduct.ImagePath,
				CreatedOn = HatProduct.CreatedOn
			};

			ProductDetailsViewModel actualResult = await this.productService.GetDetailsAsync(productId);

			Assert.That(expectedResult.Id == actualResult.Id);
			Assert.That(expectedResult.Description == actualResult.Description);
			Assert.That(expectedResult.Type == actualResult.Type);
			Assert.That(expectedResult.Price == actualResult.Price);
			Assert.That(expectedResult.ImagePath == actualResult.ImagePath);
			Assert.That(expectedResult.CreatedOn == actualResult.CreatedOn);
		}

		[Test]
		public async Task GetDetailsAsyncShouldReturnNullWithInvalidId()
		{
			int invalidProductId = 5;

			ProductDetailsViewModel actualResult = await this.productService.GetDetailsAsync(invalidProductId);

			Assert.IsNull(actualResult);
		}

		[Test]
		public async Task EditAsyncShouldEditWithValidModel()
		{
			AddEditProductViewModel editModel = new AddEditProductViewModel()
			{
				Description = "Very cute hat with a green pompon.",
				Price = 10.00m,
				TypeId = 1
			};

			await this.productServiceB.EditAsync(BannerProduct, editModel);

			Assert.That(editModel.Description == BannerProduct.Description);
			Assert.That(editModel.Price == BannerProduct.Price);
			Assert.That(editModel.TypeId == BannerProduct.TypeId);
		}

		[Test]
		public async Task DeleteAsyncShouldSetIsDeletedTrueIfValidProduct()
		{
			AddEditProductViewModel addModel = new AddEditProductViewModel()
			{
				Description = "Very cute hat with a green pompon.",
				Price = 10.00m,
				TypeId = 1
			};

			var userId = CrafterUser.Id;

			await this.productServiceB.AddAsync(userId, addModel);

			Product product = await this.productServiceB.GetProductAsync(1);

			await this.productServiceB.DeleteAsync(product);

			Assert.IsTrue(product.IsDeleted);
		}
	}
}