using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IGameResultService
    {
        Task<ResponceDto<GameResultDto>> CreateGameResult(GameResultCreateDto gameResultCreateDto);
        Task<ResponceDto<GameResultDto>> GetGameResultByGameSetupId(int id);
        Task<ResponceDto<List<GameResultDto>>> GetGameResultsByUserId(Guid UserId);
        Task<ResponceDto<GameResultDto>> UpdateGameResult(GameResultUpdateDto gameResultUpdateDto);
    }
}
