using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Helpers;
using Shared;
using Shared.DTO;

namespace Services
{
    public class GameSetupService : IGameSetupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameSetupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponceDto<GameSetupDto>> CreateGameSetup(GameSetupCreateDto gameSetupCreateDto)
        {
            try
            {
                string[] gDoors = SD.DoorValues;
                GameSetup gameSetup = new GameSetup
                {
                    GameRequestId = gameSetupCreateDto.GameRequestId,
                    GameRequestNo = gameSetupCreateDto.GameRequestNo,
                    FirstDoor = GameSetupHelper.GetDoorValue(gDoors, out gDoors),
                    SecondDoor = GameSetupHelper.GetDoorValue(gDoors, out gDoors),
                    ThirdDoor = GameSetupHelper.GetDoorValue(gDoors, out gDoors),
                };

                await _unitOfWork.GameSetup.AddAsync(gameSetup);
                _unitOfWork.SaveAsync();

                return new ResponceDto<GameSetupDto>()
                {
                    IsSuccess = true,
                    Message = "Game setup created",
                    Data = _mapper.Map<GameSetupDto>(gameSetup)
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<GameSetupDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponceDto<GameSetupDto>> GetGameSetupById(int id)
        {
            try
            {
                var gameSetup = await _unitOfWork.GameSetup.GetFirstOrDefaultAsync(a => a.Id == id,includeProperties:"User");
                return new ResponceDto<GameSetupDto>
                {
                    IsSuccess = true,
                    Data = _mapper.Map<GameSetupDto>(gameSetup),
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<GameSetupDto>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponceDto<List<GameSetupDto>>> GetGameSetupsByRequestId(int requestId)
        {
            try
            {
                var gameSetups = await _unitOfWork.GameSetup.GetAllAsync(a => a.GameRequestId == requestId, includeProperties: "GameRequest");
                return new ResponceDto<List<GameSetupDto>>
                {
                    Data = _mapper.Map<List<GameSetupDto>>(gameSetups),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {

                return new ResponceDto<List<GameSetupDto>>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

    }
}
