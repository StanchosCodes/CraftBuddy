using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CraftBuddy.Common.GeneralConstants;

namespace CraftBuddy.Web.Areas.Client.Controllers
{
	[Area(ClientAreaName)]
	[Authorize(Roles = ClientRoleName)]
	public class BaseClientController : Controller
	{

	}
}
