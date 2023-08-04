using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Product;

namespace CraftBuddy.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllAsync(Guid userId);

        Task<IEnumerable<OrderViewModel>> GetAllWaitingAsync(Guid userId);

        Task<IEnumerable<OrderViewModel>> GetAllCraftedAsync(Guid userId);

        Task<IEnumerable<CrafterViewModel>> GetCraftersAsync();

        Task AddCustomAsync(Guid clientId, AddEditCustomOrderViewModel addCustomOrderModel);

        Task AddAsync(Guid clientId, int productId, AddOrderViewModel addOrderModel);

        Task<IEnumerable<OrderStatusViewModel>> GetOrderStatusesAsync();

        Task<Order> GetOrderAsync(Guid id);

        Task<ProductOrder> GetProductOrderAsync(Guid orderId);

        Task EditAsync(Order orderToEdit, AddEditCustomOrderViewModel editCustomOrderModel);

        Task<OrderDetailsViewModel> GetDetailsAsync(Guid id, Guid userId, string username, ProductDetailsViewModel product);
    }
}
