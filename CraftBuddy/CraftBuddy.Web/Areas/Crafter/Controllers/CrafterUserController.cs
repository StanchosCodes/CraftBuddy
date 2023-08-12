using CraftBuddy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CraftBuddy.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using CraftBuddy.Services.Data.Interfaces;
using static CraftBuddy.Common.GeneralConstants;

namespace CraftBuddy.Web.Areas.Crafter.Controllers
{
	public class CrafterUserController : BaseCrafterController
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public CrafterUserController(UserManager<ApplicationUser> userManager,
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
				return RedirectToAction("Index", "Home", new { Area = CrafterAreaName });
			}

			CrafterRegisterViewModel registerModel = new CrafterRegisterViewModel();

			return View(registerModel);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(CrafterRegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			ApplicationUser user = new ApplicationUser()
			{
				UserName = model.Username,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				IsCrafter = true
			};

			IdentityResult result = await userManager.CreateAsync(user, model.Password);

			IdentityRole<Guid> crafterRole = await roleManager.FindByNameAsync(CrafterRoleName);

			await userManager.AddToRoleAsync(user, crafterRole.Name);


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
