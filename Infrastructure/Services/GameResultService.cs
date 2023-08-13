using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Shared.DTO;
using System;

namespace Services
{
    public class GameResultService : IGameResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameResultService( IUnitOfWork unitOfWork, IMapper mapper)
        {
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
                int firstChoiceDoorIndex = gameResultCreateDto.FirstChoice - 1;
                int gOpenDoorIndex = 0;

                for (int i = 0; i < gDoors.Length; i++)
                {
                    if (gDoors[i] == "C")
                    {
                        winningDoorIndex = i;
                        break;
                    }
                }

                if (firstChoiceDoorIndex == winningDoorIndex)
                {
                    // Calculate the size of the new array (original size - 1)
                    int newSize = gDoors.Length - 1;

                    // Create a new array with the calculated size
                    string[] newArray = new string[newSize];

                    // Copy elements from the original array to the new array, skipping the specified index
                    for (int i = 0, j = 0; i < gDoors.Length; i++)
                    {
                        if (i != winningDoorIndex)
                        {
                            newArray[j] = gDoors[i];
                            j++;
                        }
                    }
                    Random random = new Random();
                    gOpenDoorIndex = random.Next(0, newArray.Length)-1;
                   
                }
                else
                {
                    for (int i = 0; i < gDoors.Length; i++)
                    {
                        if (i != firstChoiceDoorIndex && i != winningDoorIndex)
                        {
                            gOpenDoorIndex = i;
                            break;
                        }
                    }
                }


                GameResult gameResult = new();
                gameResult.GameSetupId = gameResultCreateDto.GameSetupId;
                gameResult.FirstChoice = gameResultCreateDto.FirstChoice;
                gameResult.OpenedDoorNo = gOpenDoorIndex + 1;
                gameResult.SecondChoice = 0;
                await _unitOfWork.GameResult.AddAsync(gameResult);
                _unitOfWork.SaveAsync();

                return new ResponceDto<GameResultDto>()
                {
                    IsSuccess = true,
                    Message = $"I will show you what behind the door no {gOpenDoorIndex + 1}, Do you wanna switch the doors?",
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

        public async Task<ResponceDto<List<GameResultSummeryDto>>> GetGameResultSummeryByGameRequestId(int requestId)
        {
            try
            {
                var gameResults = await _unitOfWork.GameResult.GetAllAsync(a => a.GameSetup.GameRequestId == requestId,
                                                                           includeProperties: "GameSetup,GameSetup.GameRequest");
                var gameResultSummeryDtos = gameResults.Select(s => new GameResultSummeryDto
                {
                    GameSetupId = s.GameSetupId,
                    FirstDoor = s.GameSetup.FirstDoor == "C" ? "Car" : "Goat",
                    SecondDoor = s.GameSetup.SecondDoor == "C" ? "Car" : "Goat",
                    ThirdDoor = s.GameSetup.ThirdDoor == "C" ? "Car" : "Goat",
                    FirstChoice = $"{s.FirstChoice} Door",
                    OpenedDoorNo = $"{s.OpenedDoorNo} Door",
                    SecondChoice = $"{s.SecondChoice} Door",
                    WinStatus = s.IsWin == true ? "You Won" : "You Lost",
                });

                return new ResponceDto<List<GameResultSummeryDto>>
                {
                    IsSuccess = true,
                    Data = gameResultSummeryDtos.ToList(),
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<List<GameResultSummeryDto>>()
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
                var gameResult = await _unitOfWork.GameResult.GetFirstOrDefaultAsync(a => a.Id == gameResultUpdateDto.Id, includeProperties: "GameSetup");
                string[] gDoors = { gameResult.GameSetup.FirstDoor, gameResult.GameSetup.SecondDoor, gameResult.GameSetup.ThirdDoor };

                int winningDoorIndex = 0;
                int firstChoiceIndex = gameResult.FirstChoice - 1;
                int finalDoorIndex = 0;
                int openedDoorIndex = gameResult.OpenedDoorNo - 1;

                for (int i = 0; i < gDoors.Length; i++)
                {
                    if (gDoors[i] == "C")
                    {
                        winningDoorIndex = i;
                        break;
                    }
                }

                if (gameResultUpdateDto.IsSwitch)
                {
                    for (int i = 0; i < gDoors.Length; i++)
                    {
                        if (i != firstChoiceIndex && i != openedDoorIndex)
                        {
                            gameResult.SecondChoice = i + 1;
                            finalDoorIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    gameResult.SecondChoice = gameResult.FirstChoice;
                    finalDoorIndex = firstChoiceIndex;
                }

                gameResult.IsWin = finalDoorIndex == winningDoorIndex;
                _unitOfWork.SaveAsync();

                string tmpMessage = string.Empty;
                if (gameResult.IsWin) tmpMessage = "Congratulations..! You win the car.";
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
    }
}
