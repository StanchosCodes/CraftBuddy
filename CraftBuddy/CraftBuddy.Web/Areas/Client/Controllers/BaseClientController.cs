using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static CraftBuddy.Common.GeneralConstants;

namespace CraftBuddy.Web.Areas.Client.Controllers
{
	[Area(ClientAreaName)]
	[Authorize(Roles = ClientRoleName)]
	public class BaseClientController : Controller
	{

	}
}
