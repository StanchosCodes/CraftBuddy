using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CraftBuddy.Web.ViewModels;

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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 400)
			{
				return View("BadRequest");
			}
			else if (statusCode == 401)
			{
				return View("Unauthorised");
			}
			else if (statusCode == 404)
			{
				return View("NotFound");
			}

			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}