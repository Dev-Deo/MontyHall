using Shared.DTO;

namespace Domain.Interfaces
{
    public interface IGameRequestService
    {
        Task<ResponceDto<GameRequestDto>> CreateGameRequest(GameRequestCreateDto gameRequestCreateDto);
        Task<ResponceDto<List<GameRequestDto>>> GetGameRequestByUserId(Guid id);
        Task<ResponceDto<GameRequestDto>> GetGameRequestById(int id);
    }
}
