using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;

namespace Services
{
    public class GameResultService : IGameResultService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameResultService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponceDto<GameResultDto>> CreateGameResult(GameResultCreateDto gameResultCreateDto)
        {
            try
            {
                var gameSetup = await _unitOfWork.GameSetup.GetFirstOrDefaultAsync(s => s.Id == gameResultCreateDto.GameSetupId);
                GameResult gameResult = new()
                {
                    GameSetupId = gameResultCreateDto.GameSetupId,
                    FirstChoice = gameResultCreateDto.FirstChoise,
                    SecondChoice = 0,
                };
                await _unitOfWork.GameResult.AddAsync(gameResult);
                _unitOfWork.SaveAsync();

                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = true,
                    Message = "Game setup created",
                    Data = _mapper.Map<GameResultDto>(gameResult)
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponceDto<GameResultDto>> UpdateGameResult(GameResultUpdateDto gameResultUpdateDto)
        {
            try
            {
                var gameResult = await _unitOfWork.GameResult.GetAsync(gameResultUpdateDto.Id);
                gameResult.SecondChoice = gameResultUpdateDto.SecondChoice;
                _unitOfWork.SaveAsync();

                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = true,
                    Data = _mapper.Map<GameResultDto>(gameResult),
                    Message = "Game Result updated"
                };

            }
            catch (Exception ex)
            {
                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponceDto<GameResultDto>> GetGameResultsBySetupId(int setupId)
        {
            try
            {
                var result = await _unitOfWork.GameResult.GetFirstOrDefaultAsync(a => a.GameSetupId == setupId, 
                                                        includeProperties: "GameSetup,GameSetup.GameRequest");
                return new ResponceDto<GameResultDto>
                {
                    IsSuccess = true,
                    Data = _mapper.Map<GameResultDto>(result),
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<GameResultDto>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                }; ;
            }
        }

        public async Task<ResponceDto<List<GameResultDto>>> GetGameResultByGameRequestId(int requestId)
        {
            try
            {
                var gameResults = await _unitOfWork.GameResult.GetAllAsync(a => a.GameSetup.GameRequestId == requestId,
                                                                           includeProperties: "GameSetup,GameSetup.GameRequest");
                return new ResponceDto<List<GameResultDto>>
                {
                    IsSuccess = true,
                    Data = _mapper.Map<List<GameResultDto>>(gameResults),
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<List<GameResultDto>>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                }; 
            }
        }

    }
}
