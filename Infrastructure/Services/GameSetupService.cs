using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.DTO;
using Shared.Enums;
using System;

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
                string[] gDoors = { "G", "G", "C" };

                GameSetup gameSetup = new GameSetup
                {
                    GameRequestId = gameSetupCreateDto.GameRequestId,
                    GameRequestNo = gameSetupCreateDto.GameRequestNo,
                    FirstDoor = getDoor(gDoors, out gDoors),
                    SecondDoor = getDoor(gDoors, out gDoors),
                    ThirdDoor = getDoor(gDoors, out gDoors),
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

        private string getDoor(string[] cInDoors, out string[] cOutDoors)
        {
            Random random = new Random();
            int indexToSkip = random.Next(0, cInDoors.Length);
            string val = cInDoors[indexToSkip];
            int newArraySize = cInDoors.Length - 1;

            string[] newDoorsArry = new string[newArraySize];

            for (int i = 0, j = 0; i < cInDoors.Length; i++)
            {
                if (i != indexToSkip)
                {
                    newDoorsArry[j] = cInDoors[i];
                    j++;
                }
            }
            cOutDoors = newDoorsArry;
            return val;
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
