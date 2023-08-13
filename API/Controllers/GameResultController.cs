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

        [HttpGet("getGameResultsByRequestId/{id:int}", Name = "GetGameResultsByRequestIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<List<GameResultDto>>>> GetGameResultsByGameRequestIdAsync(int id)
        {
            var result = await _gameResultService.GetGameResultByGameRequestId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getGameResultBySetupId/{id:guid}", Name = "GetGameResultBySetupIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameResultDto>>> GetGameResultBySetupIdAsync(int id)
        {
            var result = await _gameResultService.GetGameResultsBySetupId(id);
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

    }
}
