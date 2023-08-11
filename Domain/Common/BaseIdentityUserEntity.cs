using Microsoft.AspNetCore.Identity;

namespace Domain.Common
{
    public abstract class BaseIdentityUserEntity : IdentityUser<Guid>
    {
    }
}
