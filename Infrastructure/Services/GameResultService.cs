using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
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
                string[] gDoors = { gameSetup.FirstDoor, gameSetup.SecondDoor, gameSetup.ThirdDoor };
                int winningDoorIndex = 0;
                int doorToOpen = 0;
                int selectedDoorIndex = gameResultCreateDto.FirstChoise;

                if (gameSetup.FirstDoor == "C") winningDoorIndex = 1;
                else if (gameSetup.FirstDoor == "C") winningDoorIndex = 2;
                else winningDoorIndex = 3;

                if (selectedDoorIndex == winningDoorIndex)
                {
                    //Remove winning door from array
                    GameSetupHelper.GetDoorNo(winningDoorIndex, gDoors, out gDoors);
                    Random random = new Random();
                    doorToOpen = random.Next(0, gDoors.Length);
                }
                else
                {
                    string tmpSelectedDoor = gDoors[selectedDoorIndex];
                    string tmpWinningDoor = gDoors[winningDoorIndex];

                    for (int i = 0; i < gDoors.Length; i++)
                    {
                        string tmpDoor = gDoors[i];
                        if (tmpDoor != tmpSelectedDoor && tmpDoor != tmpWinningDoor)
                        {
                            doorToOpen = i;
                            break;
                        }
                    }
                }


                GameResult gameResult = new();
                gameResult.GameSetupId = gameResultCreateDto.GameSetupId;
                gameResult.FirstChoice = gameResultCreateDto.FirstChoise;
                gameResult.OpenedDoorNo = doorToOpen;
                gameResult.SecondChoice = 0;
                await _unitOfWork.GameResult.AddAsync(gameResult);
                _unitOfWork.SaveAsync();

                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = true,
                    Message = $"I will show you what behind the door no {doorToOpen}, Do you wanna switch the doors?",
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
                var gameResult = await _unitOfWork.GameResult.GetFirstOrDefaultAsync(a => a.Id == gameResultUpdateDto.Id, includeProperties: "GameSetup");
                gameResult.SecondChoice = gameResultUpdateDto.SecondChoice;

                var gameSetup = gameResult.GameSetup;
                switch (gameResultUpdateDto.SecondChoice)
                {
                    case 1:
                        if (gameSetup.FirstDoor == "C") gameResult.IsWin = true;
                        else gameResult.IsWin = false;
                    break;
                    case 2:
                        if (gameSetup.SecondDoor == "C") gameResult.IsWin = true;
                        else gameResult.IsWin = false;
                        break;
                    case 3:
                        if (gameSetup.ThirdDoor == "C") gameResult.IsWin = true;
                        else gameResult.IsWin = false;
                        break;
                    default:
                        return new ResponceDto<GameResultDto>()
                        {
                            IsSuccess = false,
                            Message = "Invalid Door Selection."
                        };
                }
                _unitOfWork.SaveAsync();

                string tmpMessage = string.Empty;
                if (gameResult.IsWin)tmpMessage = "Congratulations..! You win the car.";
                else tmpMessage = "You loose this time."; 

                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = true,
                    Data = _mapper.Map<GameResultDto>(gameResult),
                    Message = tmpMessage
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
