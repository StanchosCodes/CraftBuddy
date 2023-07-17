using CraftBuddy.Web.ViewModels.Order;

namespace CraftBuddy.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllAsync(Guid userId);
    }
}
