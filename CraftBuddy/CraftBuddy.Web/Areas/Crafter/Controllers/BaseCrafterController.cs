using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static CraftBuddy.Common.GeneralConstants;

namespace CraftBuddy.Web.Areas.Crafter.Controllers
{
	[Area(CrafterAreaName)]
	[Authorize(Roles = CrafterRoleName)]
	public class BaseCrafterController : Controller
	{

	}
}
