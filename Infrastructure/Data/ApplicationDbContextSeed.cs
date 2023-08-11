
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

        public static async Task SeedAsync(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            UserManager<ApplicationUser> userManager
        )
        {
            try
            {
                //var user = await userManager.FindByEmailAsync("admin" + SD.Domain);

                //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //var specializations = JsonSerializer.Deserialize<List<Specialization>>(
                //    File.ReadAllText(path + @"/Data/SeedData/specializations.json")
                //);

                //foreach (var specialization in specializations)
                //{
                //    specialization.CreatedBy = user.Id;
                //    specialization.CreatedOn = DateTime.UtcNow;
                //    specialization.LastModifiedBy = user.Id;
                //    specialization.LastModifiedOn = DateTime.UtcNow;
                //}

                //if (!context.Specializations.Any())
                //{
                //    context.Specializations.AddRange(specializations);
                //    await context.SaveChangesAsync();
                //}
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
