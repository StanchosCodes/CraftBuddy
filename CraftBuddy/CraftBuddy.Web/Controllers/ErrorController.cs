using Microsoft.AspNetCore.Mvc;

namespace CraftBuddy.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
        
        public IActionResult BadRequest()
        {
            return View();
        }
        
        public IActionResult Unauthorised()
        {
            return View();
        }
    }
}
