using CraftBuddy.Data.Models;
using CraftBuddy.Services.Data.Interfaces;
using CraftBuddy.Web.ViewModels.Order;
using CraftBuddy.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CraftBuddy.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
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

        [HttpGet]
        public async Task<IActionResult> AllCrafted()
        {
            var currentUserId = this.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                return View("Unauthorised");
            }

            Guid userId = Guid.Parse(currentUserId!);

            IEnumerable<OrderViewModel> craftedOrders = await this.orderService.GetAllCraftedAsync(userId);

            return View("All", craftedOrders);
        }

        [HttpGet]
        public async Task<IActionResult> AddCustom()
        {
            if (!this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<CrafterViewModel> crafters = await this.orderService.GetCraftersAsync();
            IEnumerable<ProductTypeViewModel> productTypes = await this.productService.GetProductTypesAsync();

            AddEditCustomOrderViewModel addOrderModel = new AddEditCustomOrderViewModel()
            {
                Crafters = crafters,
                ProductTypes = productTypes
            };

            return View(addOrderModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustom(AddEditCustomOrderViewModel addCustomOrderModel)
        {
            if (!ModelState.IsValid)
            {
                addCustomOrderModel.Crafters = await this.orderService.GetCraftersAsync();
                addCustomOrderModel.ProductTypes = await this.productService.GetProductTypesAsync();

                return View(addCustomOrderModel);
            }

            var currentUserId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(currentUserId!);

            try
            {
                await this.orderService.AddCustomAsync(userId, addCustomOrderModel);

                return RedirectToAction("All", "Order");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to make order!");
                return View(addCustomOrderModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            if (!this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            Product productToOrder = await this.productService.GetProductAsync(id);

            if (productToOrder == null)
            {
                return View("BadRequest");
            }

            AddOrderViewModel addOrderModel = new AddOrderViewModel();

            return View(addOrderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, AddOrderViewModel addOrderModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addOrderModel);
            }

            var currentUserId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                return View("Unauthorised");
            }

            Guid userId = Guid.Parse(currentUserId!);

            try
            {
                await this.orderService.AddAsync(userId, id, addOrderModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to order the product!");

                return View(addOrderModel);
            }

            return RedirectToAction("All", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            ProductOrder productOrder = await this.orderService.GetProductOrderAsync(id);

            if (productOrder == null)
            {
                return View("BadRequest");
            }

            Order order = await this.orderService.GetOrderAsync(id);

            var currentUserId = this.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(currentUserId!);

            ProductDetailsViewModel productDetails = await this.productService.GetDetailsAsync(productOrder.ProductId);

            string? username = User?.Identity?.Name;

            if (order.ClientId != userId && username != productDetails.Crafter)
            {
                return View("Unauthorised");
            }


            OrderDetailsViewModel orderDetails = await this.orderService.GetDetailsAsync(id, userId, username!, productDetails);

            if (orderDetails == null)
            {
                return View("BadRequest");
            }

            return View(orderDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!this.User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            Order orderToEdit = await this.orderService.GetOrderAsync(id);

            if (orderToEdit == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(currentUserId!);

            ProductOrder productOrder = await this.orderService.GetProductOrderAsync(id);

            if (productOrder.Product.CrafterId != userId)
            {
                return View("Unauthorised");
            }

            IEnumerable<OrderStatusViewModel> orderStatuses = await this.orderService.GetOrderStatusesAsync();

            AddEditCustomOrderViewModel editCustomOrderModel = new AddEditCustomOrderViewModel()
            {
                OrderStatuses = orderStatuses,
                StatusId = orderToEdit.StatusId,
                Amount = orderToEdit.Amount ?? 0,
                DeliveryAddress = orderToEdit.DeliveryAddress,
                ClientPhoneNumber = orderToEdit.ClientPhoneNumber,
                Description = productOrder.Product.Description
            };

            return View(editCustomOrderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AddEditCustomOrderViewModel editCustomOrderModel)
        {
            if (!ModelState.IsValid)
            {
                editCustomOrderModel.OrderStatuses = await this.orderService.GetOrderStatusesAsync();

                return View(editCustomOrderModel);
            }

            Order orderToEdit = await this.orderService.GetOrderAsync(id);

            if (orderToEdit == null)
            {
                return View("BadRequest");
            }

            var currentUserId = this.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(currentUserId!);

            ProductOrder productOrder = await this.orderService.GetProductOrderAsync(id);

            if (productOrder.Product.CrafterId != userId)
            {
                return View("Unauthorised");
            }

            await this.orderService.EditAsync(orderToEdit, editCustomOrderModel);

            return RedirectToAction("AllWaiting", "Order");
        }
    }
}
