using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CraftBuddy.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var currentUserId = this.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                return View("Unauthorised");
            }

            Guid userId = Guid.Parse(currentUserId!);

            IEnumerable<OrderViewModel> orders = await this.orderService.GetAllAsync(userId);

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> AllWaiting()
        {
            var currentUserId = this.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                return View("Unauthorised");
            }

            Guid userId = Guid.Parse(currentUserId!);

            IEnumerable<OrderViewModel> waitingOrders = await this.orderService.GetAllWaitingAsync(userId);

            return View("All", waitingOrders);
        }
    }
}
