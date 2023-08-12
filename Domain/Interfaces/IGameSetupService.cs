using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IGameSetupService
    {
        Task<ResponceDto<GameSetupDto>> CreateGameSetup(GameSetupCreateDto gameSetupCreateDto);
        Task<ResponceDto<List<GameSetupDto>>> GetGameSetupsByRequestId(int requestId);
        Task<ResponceDto<GameSetupDto>> GetGameSetupById(int id);

    }
}
