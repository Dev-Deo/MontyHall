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
                GameResult gameResult = _mapper.Map<GameResult>(gameResultCreateDto);
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

        public async Task<ResponceDto<GameResultDto>> GetGameResultByGameSetupId(int id)
        {
            try
            {
                var result = await _unitOfWork.GameResult.GetFirstOrDefaultAsync(a => a.GameSetupId == id,includeProperties: "GameSetup,GameSetup.User");
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

        public async Task<ResponceDto<List<GameResultDto>>> GetGameResultsByUserId(Guid UserId)
        {
            try
            {
                var gameResults = await _unitOfWork.GameResult.GetAllAsync(a => a.GameSetup.UserId == UserId,
                                                                           includeProperties: "GameSetup,GameSetup.User");
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

        public async Task<ResponceDto<GameResultDto>> UpdateGameResult(GameResultUpdateDto gameResultUpdateDto)
        {
            try
            {
                var gameResult = await _unitOfWork.GameResult.GetAsync(gameResultUpdateDto.Id);
                gameResult.FirstChoice = gameResultUpdateDto.FirstChoice;
                gameResult.SecondChoice = gameResultUpdateDto.SecondChoice;
                gameResult.GameSetupId = gameResultUpdateDto.GameSetupId;
                gameResult.IsWin = gameResultUpdateDto.IsWin ?? false;
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
    }
}
