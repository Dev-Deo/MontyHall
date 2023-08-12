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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameSetupService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region USER GAME ATTEMPT
        public async Task<ResponceDto<ApplicationUserDto>> CreateUserAttempt(UserAttemptCreateDto userAttemptCreateDto)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userAttemptCreateDto.UserId);
                user.TotalAttempt = userAttemptCreateDto.TotalAttempt;
                var result = await _userManager.UpdateAsync(user);
                return new ResponceDto<ApplicationUserDto>()
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Errors.FirstOrDefault()?.Description,
                    Data = _mapper.Map<ApplicationUserDto>(user)
                    //Data = new ApplicationUserDto
                    //{
                    //    Id = user.Id,
                    //    Email = user.Email,
                    //    FirstName = user.FirstName,
                    //    LastName = user.LastName,
                    //    TotalAttempt = user.TotalAttempt
                    //}
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<ApplicationUserDto>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponceDto<ApplicationUserDto>> GetUserAttemptByUserId(Guid id)
        {
            try
            {
                var user = await _userManager.Users.Select(s => new ApplicationUserDto
                {
                    Id = s.Id,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    TotalAttempt = s.TotalAttempt

                }).FirstOrDefaultAsync(u => u.Id == id);

                return new ResponceDto<ApplicationUserDto>
                {
                    Data = user,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<ApplicationUserDto>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                }; ;
            }
        }
        #endregion 

        #region GAME SETUP
        public async Task<ResponceDto<GameSetupDto>> CreateGameSetup(GameSetupCreateDto gameSetupCreateDto)
        {
            try
            {
                string[] gDoors = { "G", "G", "C" };

                GameSetup gameSetup = new GameSetup
                {
                    AttemptNo = gameSetupCreateDto.AttemptNo,
                    UserId = gameSetupCreateDto.UserId,
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

        public async Task<ResponceDto<List<GameSetupDto>>> GetGameSetupsByUserId(Guid UserId)
        {
            try
            {
                var gameSetups = await _unitOfWork.GameSetup.GetAllAsync(a => a.UserId == UserId,includeProperties:"User");
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
        #endregion
    }
}
