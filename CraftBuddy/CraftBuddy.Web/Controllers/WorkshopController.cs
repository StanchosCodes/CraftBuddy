using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Workshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CraftBuddy.Web.Infrastructure.Extensions;
using static CraftBuddy.Common.GeneralConstants;

namespace CraftBuddy.Web.Controllers
{
    [Authorize]
    public class WorkshopController : Controller
    {
        private readonly IWorkshopService workshopService;

        public WorkshopController(IWorkshopService workshopService)
        {
            this.workshopService = workshopService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<WorkshopViewModel> workshops = await this.workshopService.GetAllAsync();

            return View(workshops);
        }

        [HttpGet]
        [Authorize(Roles = CrafterRoleName)]
        public IActionResult Add()
        {
            AddEditWorkshopViewModel workshopModel = new AddEditWorkshopViewModel();

            return View(workshopModel);
        }

        [HttpPost]
        [Authorize(Roles = CrafterRoleName)]
        public async Task<IActionResult> Add(AddEditWorkshopViewModel workshopModel)
        {
            if (!ModelState.IsValid)
            {
                return View(workshopModel);
            }

            var currentUserId = this.User.GetId();
            Guid userId = Guid.Parse(currentUserId!);

            try
            {
                await this.workshopService.AddAsync(userId, workshopModel);

                return RedirectToAction("All", "Workshop");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to add workshop!");

                return View(workshopModel);
            }
        }

        [HttpGet]
        [Authorize(Roles = CrafterRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            Workshop workshopToEdit = await this.workshopService.GetWorkshopAsync(id);

            if (workshopToEdit == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            if (workshopToEdit.OrganiserId != userId)
            {
                return View("Unauthorised");
            }

            AddEditWorkshopViewModel workshopModel = new AddEditWorkshopViewModel()
            {
                Title = workshopToEdit.Title,
                Description = workshopToEdit.Description,
                StartDate = workshopToEdit.StartDate,
                EndDate = workshopToEdit.EndDate
            };

            return View(workshopModel);
        }

        [HttpPost]
        [Authorize(Roles = CrafterRoleName)]
        public async Task<IActionResult> Edit(int id, AddEditWorkshopViewModel editedWorkshop)
        {
            if (!ModelState.IsValid)
            {
                return View(editedWorkshop);
            }

            Workshop workshop = await this.workshopService.GetWorkshopAsync(id);

            if (workshop == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            if (workshop.OrganiserId != userId)
            {
                return View("Unauthorised");
            }

            await this.workshopService.EditAsync(workshop, editedWorkshop);

            return RedirectToAction("All", "Workshop");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            WorkshopDetailsViewModel workshopDetails = await this.workshopService.GetDetailsAsync(id);

            if (workshopDetails == null)
            {
                return View("NotFound");
            }

            return View(workshopDetails);
        }

        [Authorize(Roles = CrafterRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            Workshop workshopToDelete = await this.workshopService.GetWorkshopAsync(id);

            if (workshopToDelete == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            if (workshopToDelete.OrganiserId != userId)
            {
                return View("Unauthorised");
            }

            await this.workshopService.DeleteAsync(workshopToDelete);

            return RedirectToAction("All", "Workshop");
        }

        public async Task<IActionResult> Joined()
        {
            if (!this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var currentUserId = this.User!.GetId();
			Guid userId = Guid.Parse(currentUserId!);

            IEnumerable<WorkshopViewModel> joinedWorkshops = await this.workshopService.GetJoinedAsync(userId);

            return View(joinedWorkshops);
        }

        public async Task<IActionResult> Join(int id)
        {
            WorkshopViewModel workshopModel = await this.workshopService.CreateWorkshopViewModel(id);

            if (workshopModel == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User!.GetId();

			if (currentUserId == null)
            {
                return View("Unauthorised");
            }

            Guid userId = Guid.Parse(currentUserId!);

            bool isJoined = await this.workshopService.IsJoinedAsync(userId, id);

            if (isJoined)
            {
                return RedirectToAction("All", "Workshop");
            }

            Workshop workshopToJoin = await this.workshopService.GetWorkshopAsync(id);

            if (workshopToJoin == null)
            {
                return View("BadRequest");
            }

            workshopToJoin.ParticipantsCount += 1;

            await this.workshopService.JoinAsync(userId, workshopModel.Id);

            return RedirectToAction("Joined", "Workshop");
        }

        public async Task<IActionResult> Leave(int id)
        {
            WorkshopViewModel workshopModel = await this.workshopService.CreateWorkshopViewModel(id);

            if (workshopModel == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User!.GetId();

			if (currentUserId == null)
            {
                return View("Unauthorised");
            }

            Guid userId = Guid.Parse(currentUserId!);

            await this.workshopService.LeaveAsync(userId, id);

            return RedirectToAction("Joined", "Workshop");
        }
    }
}
