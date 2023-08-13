using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IGameResultService
    {
        Task<ResponceDto<GameResultDto>> CreateGameResult(GameResultCreateDto gameResultCreateDto);
        Task<ResponceDto<GameResultDto>> UpdateGameResult(GameResultUpdateDto gameResultUpdateDto);
        Task<ResponceDto<GameResultDto>> GetGameResultsBySetupId(int setupId);
        Task<ResponceDto<List<GameResultDto>>> GetGameResultByGameRequestId(int requestId);
        Task<ResponceDto<List<GameResultSummeryDto>>> GetGameResultSummeryByGameRequestId(int requestId);
    }
}
