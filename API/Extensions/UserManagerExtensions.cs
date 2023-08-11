using Domain.Entities.Identity;
using System.Security.Claims;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> FindByUserByClaimsPrincipalWithAddressAsync(
            this UserManager<ApplicationUser> input,
            ClaimsPrincipal user
        )
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<ApplicationUser> FindByEmailFromClaimsPrinciple(
            this UserManager<ApplicationUser> input,
            ClaimsPrincipal user
        )
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<ApplicationUser> FindByEmailByClaimsPrincipalWithRolesAsync(
            this UserManager<ApplicationUser> input,
            ClaimsPrincipal user
        )
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<ApplicationUser> FindByEmailWithRolesAsync(
            this UserManager<ApplicationUser> input,
            string email
        )
        {
            return await input.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
