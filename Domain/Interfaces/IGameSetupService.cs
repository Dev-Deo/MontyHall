using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IGameSetupService
    {
        Task<ResponceDto<ApplicationUserDto>> CreateUserAttempt(UserAttemptCreateDto userAttemptCreateDto);
        Task<ResponceDto<ApplicationUserDto>> GetUserAttemptByUserId(Guid id);

        Task<ResponceDto<GameSetupDto>> CreateGameSetup(GameSetupCreateDto gameSetupCreateDto);
        Task<ResponceDto<GameSetupDto>> GetGameSetupById(int id);
        Task<ResponceDto<List<GameSetupDto>>> GetGameSetupsByUserId(Guid UserId);

    }
}
