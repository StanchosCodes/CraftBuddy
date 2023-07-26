using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using static CraftBuddy.Services.Data.Utilities.ServiceUtilities;

namespace CraftBuddy.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly CraftBuddyDbContext context;

        public OrderService(CraftBuddyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(Guid userId)
        {
            IEnumerable<OrderViewModel> orders = await this.context
                .Orders
                .Where(o => o.ClientId == userId)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    Status = o.Status.Name,
                    Product = o.Products
                    .Select(po => new ProductDetailsViewModel()
                    {
                        Id = po.Product.Id,
                        Type = po.Product.Type.Name,
                        Description = po.Product.Description,
                        Price = po.Product.Price ?? 0,
                        ImagePath = po.Product.ImagePath,
                        IsCustom = po.Product.IsCustom,
                        Crafter = po.Product.Crafter.UserName,
                        CreatedOn = po.Product.CreatedOn
                    }).FirstOrDefault()!,
                    CreatedOn = o.CreatedOn.ToString("f")
                })
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllWaitingAsync(Guid userId)
        {
            IEnumerable<OrderViewModel> waitingOrders = await this.context
                .Orders
                .Where(o => (o.Status.Name == "Waiting" || o.Status.Name == "Crafting") && o.Products.Where(po => po.Product.CrafterId == userId).Any())
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    Status = o.Status.Name,
                    Product = o.Products
                    .Where(po => po.Product.IsCustom == true && po.Product.CrafterId == userId)
                    .Select(po => new ProductDetailsViewModel()
                    {
                        Id = po.Product.Id,
                        Type = po.Product.Type.Name,
                        Description = po.Product.Description,
                        Price = po.Product.Price ?? 0,
                        ImagePath = po.Product.ImagePath,
                        Crafter = po.Product.Crafter.UserName,
                        CreatedOn = po.Product.CreatedOn
                    }).FirstOrDefault()!,
                    CreatedOn = o.CreatedOn.ToString("f")
                })
                .ToListAsync();

            return waitingOrders;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllCraftedAsync(Guid userId)
        {
            IEnumerable<OrderViewModel> craftedOrders = await this.context
                .Orders
                .Where(o => o.Status.Name == "Crafted" && o.Products.Where(po => po.Product.CrafterId == userId).Any() && o.Products.Where(po => po.Product.IsCustom == true).Any())
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    Status = o.Status.Name,
                    Product = o.Products
                    .Where(po => po.Product.IsCustom == true && po.Product.CrafterId == userId)
                    .Select(po => new ProductDetailsViewModel()
                    {
                        Id = po.Product.Id,
                        Type = po.Product.Type.Name,
                        Description = po.Product.Description,
                        Price = po.Product.Price ?? 0,
                        ImagePath = po.Product.ImagePath,
                        Crafter = po.Product.Crafter.UserName,
                        CreatedOn = po.Product.CreatedOn
                    }).FirstOrDefault()!,
                    CreatedOn = o.CreatedOn.ToString("f")
                })
                .ToListAsync();

            return craftedOrders;
        }

        public async Task<IEnumerable<CrafterViewModel>> GetCraftersAsync()
        {
            IEnumerable<CrafterViewModel> crafters = await this.context
                .Users
                .Select(u => new CrafterViewModel()
                {
                    Id = u.Id,
                    Username = u.UserName
                })
                .ToListAsync();

            return crafters;
        }

        public async Task AddCustomAsync(Guid clientId, AddEditCustomOrderViewModel addCustomOrderModel)
        {
            try
            {
                Order newOrder = new Order()
                {
                    ClientId = clientId,
                    DeliveryAddress = addCustomOrderModel.DeliveryAddress,
                    ClientPhoneNumber = addCustomOrderModel.ClientPhoneNumber
                };

                Product newProduct = new Product()
                {
                    TypeId = addCustomOrderModel.ProductTypeId,
                    Description = addCustomOrderModel.Description,
                    CrafterId = addCustomOrderModel.CrafterId,
                    IsCustom = true,
                    ImagePath = ChangeImagePath(addCustomOrderModel.ProductTypeId)
                };

                ProductOrder newProductOrder = new ProductOrder()
                {
                    Product = newProduct,
                    Order = newOrder
                };

                await this.context.Orders.AddAsync(newOrder);
                await this.context.Products.AddAsync(newProduct);

                await this.context.ProductsOrders.AddAsync(newProductOrder);
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task AddAsync(Guid clientId, int productId, AddOrderViewModel addOrderModel)
        {
            try
            {
                Product? productToOrder = await this.context
                    .Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

                Order newOrder = new Order()
                {
                    ClientId = clientId,
                    ClientPhoneNumber = addOrderModel.ClientPhoneNumber,
                    DeliveryAddress = addOrderModel.DeliveryAddress,
                    Amount = productToOrder!.Price,
                    StatusId = 3 // Crafted
                };

                ProductOrder newProductOrder = new ProductOrder()
                {
                    Product = productToOrder,
                    Order = newOrder
                };

                await this.context.Orders.AddAsync(newOrder);
                await this.context.ProductsOrders.AddAsync(newProductOrder);

                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<IEnumerable<OrderStatusViewModel>> GetOrderStatusesAsync()
        {
            IEnumerable<OrderStatusViewModel> orderStatuses = await this.context
                .OrderStatuses
                .Select(os => new OrderStatusViewModel()
                {
                    Id = os.Id,
                    Name = os.Name
                })
                .ToListAsync();

            return orderStatuses;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            Order? order = await this.context
                .Orders
                .FindAsync(id);

#pragma warning disable CS8603 // Possible null reference return.
            return order;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<ProductOrder> GetProductOrderAsync(int orderId)
        {
            ProductOrder? productOrder = await this.context
                .ProductsOrders
                .Include(p => p.Product)
                .FirstOrDefaultAsync(po => po.OrderId == orderId);

#pragma warning disable CS8603 // Possible null reference return.
            return productOrder;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task EditAsync(Order orderToEdit, AddEditCustomOrderViewModel editCustomOrderModel)
        {
            orderToEdit.StatusId = editCustomOrderModel.StatusId;
            orderToEdit.Amount = editCustomOrderModel.Amount;

            await this.context.SaveChangesAsync();
        }

        public async Task<OrderDetailsViewModel> GetDetailsAsync(int id, Guid userId, string username, ProductDetailsViewModel product)
        {
            OrderDetailsViewModel? orderDetails = await this.context
                .Orders
                .Where(o => o.Id == id && (o.ClientId == userId || username == product.Crafter))
                .Select(o => new OrderDetailsViewModel()
                {
                    Id = o.Id,
                    Amount = o.Amount ?? 0,
                    Status = o.Status.Name,
                    DeliveryAddress = o.DeliveryAddress,
                    ClientPhoneNumber = o.ClientPhoneNumber,
                    CreatedOn = o.CreatedOn.ToString("f"),
                    Type = product.Type,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                    Crafter = product.Crafter
                })
                .FirstOrDefaultAsync();

            return orderDetails!;
        }
    }
}
