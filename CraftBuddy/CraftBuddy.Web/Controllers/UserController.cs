using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CraftBuddy.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public UserController(UserManager<ApplicationUser> userManager,
							  SignInManager<ApplicationUser> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
        }

        [HttpGet]
		public IActionResult Login()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("Index", "Home");
			}

			UserLoginViewModel loginModel = new UserLoginViewModel();

			return View(loginModel);
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await this.userManager.FindByNameAsync(model.Username);

			if (user != null)
			{
				var result = await this.signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);


				if (result.Succeeded)
				{
					return RedirectToAction("All", "Product", new { Area = "" });
				}
			}

			ModelState.AddModelError("", "Invalid Login");

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}
