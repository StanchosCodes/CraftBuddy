using Microsoft.AspNetCore.Mvc;

namespace CraftBuddy.Web.Areas.Crafter.Controllers
{
	public class CrafterUserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
