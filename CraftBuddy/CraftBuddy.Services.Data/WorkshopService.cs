using CraftBuddy.Data;
using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Workshop;
using Microsoft.EntityFrameworkCore;
using static CraftBuddy.Common.ImagePathConstants.ImagePath;

namespace CraftBuddy.Services.Data
{
    public class WorkshopService : IWorkshopService
    {
        private readonly CraftBuddyDbContext context;

        public WorkshopService(CraftBuddyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<WorkshopViewModel>> GetAllAsync()
        {
            IEnumerable<WorkshopViewModel> workshops = await this.context
                .Workshops
                .Where(w => w.IsDeleted == false)
                .Select(w => new WorkshopViewModel()
                {
                    Id = w.Id,
                    Title = w.Title,
                    StartDate = w.StartDate.ToString("f"),
                    ImagePath = w.ImagePath,
                    Organiser = w.Organiser.UserName,
                    ParticipantsCount = w.ParticipantsCount
                })
                .ToListAsync();

            return workshops;
        }

        public async Task AddAsync(Guid userId, AddEditWorkshopViewModel workshopModel)
        {
            try
            {
                Workshop newWorkshop = new Workshop()
                {
                    Title = workshopModel.Title,
                    Description = workshopModel.Description,
                    StartDate = workshopModel.StartDate,
                    EndDate = workshopModel.EndDate,
                    OrganiserId = userId,
                    ImagePath = WorkshopImagePath
                };

                await this.context.Workshops.AddAsync(newWorkshop);
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<Workshop> GetWorkshopAsync(int id)
        {
            Workshop? workshop = await this.context
                .Workshops
                .Where(w => w.IsDeleted == false && w.Id == id)
                .FirstOrDefaultAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return workshop;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task EditAsync(Workshop workshopToEdit, AddEditWorkshopViewModel editedModel)
        {
            workshopToEdit.Title = editedModel.Title;
            workshopToEdit.Description = editedModel.Description;
            workshopToEdit.StartDate = editedModel.StartDate;
            workshopToEdit.EndDate = editedModel.EndDate;

            await this.context.SaveChangesAsync();
        }

        public async Task<WorkshopDetailsViewModel> GetDetailsAsync(int id)
        {
            WorkshopDetailsViewModel? workshopDetails = await this.context
                .Workshops
                .Where(w => w.Id == id && w.IsDeleted == false)
                .Select(w => new WorkshopDetailsViewModel()
                {
                    Id = w.Id,
                    Title = w.Title,
                    Description = w.Description,
                    StartDate = w.StartDate.ToString("f"),
                    EndDate = w.EndDate.ToString("f"),
                    Organiser = w.Organiser.UserName,
                    ImagePath = w.ImagePath,
                    ParticipantsCount = w.ParticipantsCount,
                    CreatedOn = w.CreatedOn.ToString("f")
                })
                .FirstOrDefaultAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return workshopDetails;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task DeleteAsync(Workshop workshopToDelete)
        {
            workshopToDelete.IsDeleted = true;

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkshopViewModel>> GetJoinedAsync(Guid userId)
        {
            IEnumerable<WorkshopViewModel> workshops = await this.context
                .WorkshopsParticipants
                .Where(wp => wp.ParticipantId == userId)
                .Select(wp => new WorkshopViewModel()
                {
                    Id = wp.Workshop.Id,
                    Title = wp.Workshop.Title,
                    StartDate = wp.Workshop.StartDate.ToString("f"),
                    Organiser = wp.Workshop.Organiser.UserName,
                    ParticipantsCount = wp.Workshop.ParticipantsCount,
                    ImagePath = wp.Workshop.ImagePath
                })
                .ToListAsync();

            return workshops;
        }

        public async Task<bool> IsJoinedAsync(Guid userId, int workshopId)
        {
            bool isJoined = await this.context.WorkshopsParticipants.AnyAsync(wp => wp.ParticipantId == userId && wp.WorkshopId == workshopId);

            return isJoined;
        }

        public async Task JoinAsync(Guid userId, int workshopId)
        {
            WorkshopParticipant workshopParticipant = new WorkshopParticipant()
            {
                WorkshopId = workshopId,
                ParticipantId = userId
            };

            await this.context.WorkshopsParticipants.AddAsync(workshopParticipant);
            await this.context.SaveChangesAsync();
        }

        public async Task<WorkshopViewModel> CreateWorkshopViewModel(int id)
        {
            WorkshopViewModel? workshopModel = await this.context
                .Workshops
                .Where(w => w.Id == id && w.IsDeleted == false)
            .Select(w => new WorkshopViewModel()
            {
                Id = w.Id,
                Title = w.Title,
                StartDate = w.StartDate.ToString("f"),
                Organiser = w.Organiser.UserName,
                ParticipantsCount = w.ParticipantsCount,
                ImagePath = w.ImagePath
            })
            .FirstOrDefaultAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return workshopModel;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task LeaveAsync(Guid userId, int workshopId)
        {
            WorkshopParticipant? workshopParticipant = await this.context
                .WorkshopsParticipants
                .FirstOrDefaultAsync(wp => wp.ParticipantId == userId && wp.WorkshopId == workshopId);

            if (workshopParticipant != null)
            {
                Workshop? workshop = await this.context
                .Workshops
                .Where(w => w.IsDeleted == false && w.Id == workshopId)
                .FirstOrDefaultAsync();

                workshop!.ParticipantsCount -= 1;

                this.context.WorkshopsParticipants.Remove(workshopParticipant);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
