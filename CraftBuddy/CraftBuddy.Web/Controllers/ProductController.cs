using Microsoft.AspNetCore.Mvc;

namespace CraftBuddy.Web.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult All()
		{
			return View();
		}
	}
}
