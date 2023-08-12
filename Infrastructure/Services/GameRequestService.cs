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
    public class GameRequestService : IGameRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponceDto<GameRequestDto>> CreateGameRequest(GameRequestCreateDto gameRequestCreateDto)
        {
            try
            {
                var request = await _unitOfWork.GameRequest.GetFirstOrDefaultAsync(u => u.UserId == gameRequestCreateDto.UserId);
                request.TotalGameRequests = request.TotalGameRequests;
                return new ResponceDto<GameRequestDto>()
                {
                    IsSuccess = true,
                    Message = "Successfully Created.",
                    Data = _mapper.Map<GameRequestDto>(request)
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<GameRequestDto>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponceDto<GameRequestDto>> GetGameRequestById(int id)
        {
            try
            {
                var gameRequest = await _unitOfWork.GameRequest.GetFirstOrDefaultAsync(a => a.Id == id, includeProperties: "User");
                return new ResponceDto<GameRequestDto>
                {
                    IsSuccess = true,
                    Data = _mapper.Map<GameRequestDto>(gameRequest),
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<GameRequestDto>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponceDto<List<GameRequestDto>>> GetGameRequestByUserId(Guid id)
        {
            try
            {
                var gameRequests = await _unitOfWork.GameRequest.GetAllAsync(a => a.UserId == id, includeProperties: "User");
                return new ResponceDto<List<GameRequestDto>>
                {
                    IsSuccess = true,
                    Data = _mapper.Map<List<GameRequestDto>>(gameRequests),
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<List<GameRequestDto>>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

    }
}
