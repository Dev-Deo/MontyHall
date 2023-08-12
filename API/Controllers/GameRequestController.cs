using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTO;

namespace API.Controllers
{
    [Authorize]
    public class GameRequestController : ApiControllerBase
    {
        private readonly IGameRequestService _gameRequestService;

        public GameRequestController(IGameRequestService gameRequestService) 
        { 
            _gameRequestService = gameRequestService;
        }

        [HttpPost("createGameRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameRequestDto>>> CreateUserAttemptAsync(GameRequestCreateDto gameRequestCreateDto)
        {
            var result = await _gameRequestService.CreateGameRequest(gameRequestCreateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("getGameRequestByUserId/{id:guid}", Name = "GetGameRequestByUserIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameRequestDto>>> GetGameRequestByUserIdAsync(Guid id)
        {
            var result = await _gameRequestService.GetGameRequestByUserId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
