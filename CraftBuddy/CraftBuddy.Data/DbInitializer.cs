using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraftBuddy.Data.Models;

namespace CraftBuddy.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<CraftBuddyDbContext>();

                string[] roles = new string[] { "Crafter", "Client" };

                foreach (string role in roles)
                {
                    var roleStore = scope.ServiceProvider.GetService<IRoleStore<IdentityRole<Guid>>>();

                    if (context != null && !context.Roles.Any(r => r.Name == role))
                    {
                        _ = roleStore?.CreateAsync(new IdentityRole<Guid>(role), CancellationToken.None).Result;
                    }
                }

                context?.SaveChangesAsync();
            }           
        }

        public static async Task AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser>? userManager = services.GetService<UserManager<ApplicationUser>>();

            if (userManager != null)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(email);
                _ = await userManager.AddToRolesAsync(user, roles);
            }
        }
    }
}
