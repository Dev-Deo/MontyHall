using System.Security.Claims;

namespace Domain.Interfaces
{
    public interface ILoggedInUserService
    {
        string Email { get; }
        ClaimsPrincipal User { get; }
    }
}
