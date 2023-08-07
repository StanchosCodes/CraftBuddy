using CraftBuddy.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CraftBuddy.Data
{
	public class RolesInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (IServiceScope? scope = serviceProvider.CreateScope())
            {
                CraftBuddyDbContext? context = scope.ServiceProvider.GetService<CraftBuddyDbContext>();

                string[] roles = new string[] { "Crafter", "Client" };

                foreach (string role in roles)
                {
                    IRoleStore<IdentityRole<Guid>>? roleStore = scope.ServiceProvider.GetService<IRoleStore<IdentityRole<Guid>>>();

                    if (context != null && !context.Roles.Any(r => r.Name == role))
                    {
                        _ = roleStore?.CreateAsync(new IdentityRole<Guid>(role), CancellationToken.None).Result;
                    }
                }

                context?.SaveChangesAsync();
            }           
        }

        public static async Task AssignRoles(IServiceProvider services, string email, string role)
        {
            UserManager<ApplicationUser>? userManager = services.GetService<UserManager<ApplicationUser>>();

            if (userManager != null)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(email);
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
