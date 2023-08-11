using Domain.Entities.Identity;
using Shared.DTO;

namespace Domain.Interfaces
{
    public interface ITokenService
    {
        ResponceDto<TokenDto> CreateToken(ApplicationUser user, TokenDto tokenDto = null);
    }
}
