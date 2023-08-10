using Microsoft.AspNetCore.Mvc;

namespace CraftBuddy.Web.Areas.Crafter.Controllers
{
	public class HomeController : BaseCrafterController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
