using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.Workshop;

namespace CraftBuddy.Services.Data.Interfaces
{
    public interface IWorkshopService
    {
        Task<IEnumerable<WorkshopViewModel>> GetAllAsync();

        Task AddAsync(Guid userId, AddEditWorkshopViewModel workshopModel);

        Task<Workshop> GetWorkshopAsync(int id);

        Task EditAsync(Workshop workshopToEdit, AddEditWorkshopViewModel editedModel);

        Task<WorkshopDetailsViewModel> GetDetailsAsync(int id);

        Task DeleteAsync(Workshop workshopToDelete);

        Task<IEnumerable<WorkshopViewModel>> GetJoinedAsync(Guid userId);

        Task<bool> IsJoinedAsync(Guid userId, int workshopId);

        Task JoinAsync(Guid userId, int workshopId);

        Task<WorkshopViewModel> CreateWorkshopViewModel(int id);

        Task LeaveAsync(Guid userId, int workshopId);
    }
}
