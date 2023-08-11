
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shared;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedUsersAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "admin" + SD.Domain,
                    UserName = "admin" + SD.Domain,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(user, "Admin@#$1234");
            }
        }

    }
}
