using System.Security.Claims;
using static CraftBuddy.Common.RolesConstants;

namespace CraftBuddy.Web.Infrastructure.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string? GetId(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		}

		public static bool IsCrafter(this ClaimsPrincipal user)
		{
			return user.IsInRole(CrafterRoleName);
		}
	}
}
