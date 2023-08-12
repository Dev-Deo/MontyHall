using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTO;

namespace API.Controllers
{
    [Authorize]
    public class GameSetupController : ApiControllerBase
    {
        private readonly IGameSetupService _gameSetupService;

        public GameSetupController(IGameSetupService gameSetupService) 
        { 
            _gameSetupService = gameSetupService;
        }


        [HttpPost("createGameSetup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameSetupDto>>> CreateGameSetup(GameSetupCreateDto gameSetupCreateDto)
        {
            var result = await _gameSetupService.CreateGameSetup(gameSetupCreateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getGameSetupById/{id:int}", Name = "GetGameSetupByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameSetupDto>>> GetGameSetupByIdAsync(int id)
        {
            var result = await _gameSetupService.GetGameSetupById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }      
        
        [HttpGet("getGameSetupsByRequestId/{id:int}", Name = "GetGameSetupsByRequestIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<List<GameSetupDto>>>> GetGameSetupsByRequestIdAsync(int id)
        {
            var result = await _gameSetupService.GetGameSetupsByRequestId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
       
    }
}
