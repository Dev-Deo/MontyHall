using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTO;

namespace API.Controllers
{
    [Authorize]
    public class GameResultController : ApiControllerBase
    {
        private readonly IGameResultService _gameResultService;

        public GameResultController(IGameResultService gameResultService) 
        { 
            _gameResultService = gameResultService;
        }

        #region GAME RESULT

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameResultDto>>> CreateGameResultAsync(GameResultCreateDto gameResultCreateDto)
        {
            var result = await _gameResultService.CreateGameResult(gameResultCreateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getGameResultByGameSetupId/{id:int}", Name = "GetGameResultByGameSetupIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameResultDto>>> GetGameResultByGameSetupIdAsync(int id)
        {
            var result = await _gameResultService.GetGameResultByGameSetupId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getGameResultsByUserId/{id:guid}", Name = "GetGameResultsByUserIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<List<GameResultDto>>>> GetGameResultsByUserIdAsync(Guid id)
        {
            var result = await _gameResultService.GetGameResultsByUserId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameResultDto>>> UpdateGameResultAsync(GameResultUpdateDto gameResultUpdateDto)
        {
            var result = await _gameResultService.UpdateGameResult(gameResultUpdateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion
    }
}
