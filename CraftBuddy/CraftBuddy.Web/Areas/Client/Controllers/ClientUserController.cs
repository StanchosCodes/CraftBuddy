using CraftBuddy.Data.Models;
using CraftBuddy.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static CraftBuddy.Common.GeneralConstants;

namespace CraftBuddy.Web.Areas.Client.Controllers
{
	public class ClientUserController : BaseClientController
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public ClientUserController(UserManager<ApplicationUser> userManager,
									SignInManager<ApplicationUser> signInManager,
									RoleManager<IdentityRole<Guid>> roleManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("Index", "Home", new { Area = ClientAreaName });
			}

			ClientRegisterViewModel registerModel = new ClientRegisterViewModel();

			return View(registerModel);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(ClientRegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			ApplicationUser user = new ApplicationUser()
			{
				UserName = model.Username,
				Email = model.Email
			};

			IdentityResult result = await userManager.CreateAsync(user, model.Password);

			IdentityRole<Guid> clientRole = await roleManager.FindByNameAsync(ClientRoleName);

			await userManager.AddToRoleAsync(user, clientRole.Name);


			if (result.Succeeded)
			{
				await this.signInManager.SignInAsync(user, false);
				return RedirectToAction("All", "Product", new { Area = "" });
			}

			foreach (var item in result.Errors)
			{
				ModelState.AddModelError("", item.Description);
			}

			return View(model);
		}
	}
}
