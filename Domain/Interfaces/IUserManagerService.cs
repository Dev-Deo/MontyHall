using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IUserManagerService
    {
        Task<ResponceDto<ApplicationUserDto>> CreateUser(ApplicationUserCreateDto applicationUserDto);
        Task<ResponceDto<bool>> DeleteApplicationUser(Guid id);
        Task<ResponceDto<ApplicationUserDto>> GetUserByIdAsync(Guid id);
        Task<ResponceDto<List<ApplicationUserDto>>> GetUsersAsync();
        Task<ResponceDto<ApplicationUserDto>> UpdateApplicationUser(ApplicationUserUpdateDto applicationUserUpdateDto);
    }

}
