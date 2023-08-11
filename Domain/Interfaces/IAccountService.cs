using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ResponceDto<CurrentUserDto>> Login(LoginDto loginDto);
    }

}
