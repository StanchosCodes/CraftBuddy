using CraftBuddy.Data;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                    Price = o.Price,
                    Status = o.Status.Name,
                    Product = o.Products.Select(po => new ProductDetailsViewModel()
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

            return orders;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllWaitingAsync(Guid userId)
        {
            IEnumerable<OrderViewModel> waitingOrders = await this.context
                .Orders
                .Where(o => o.CrafterId == userId && o.Status.Name == "Waiting")
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Price = o.Price,
                    Status = o.Status.Name,
                    Product = o.Products.Select(po => new ProductDetailsViewModel()
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
    }
}
