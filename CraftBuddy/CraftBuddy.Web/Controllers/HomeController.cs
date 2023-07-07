using CraftBuddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CraftBuddy.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (this.User?.Identity?.IsAuthenticated ?? false)
			{
				return RedirectToAction("All", "Product");
			}

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}