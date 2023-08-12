using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTO;

namespace API.Controllers
{
    public class GameSetupController : ApiControllerBase
    {
        private readonly IGameSetupService _gameSetupService;

        public GameSetupController(IGameSetupService gameSetupService) 
        { 
            _gameSetupService = gameSetupService;
        }

        #region USER GAME ATTEMPT
        [HttpGet("{id:guid}", Name = "GetUserAttemptByUserIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<ApplicationUserDto>>> GetUserAttemptByUserIdAsync(Guid id)
        {
            var gameAttempt = await _gameSetupService.GetUserAttemptByUserId(id);
            if (!gameAttempt.IsSuccess)
            {
                return BadRequest(gameAttempt);
            }
            return Ok(gameAttempt);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<List<ApplicationUserDto>>>> CreateUserAttemptAsync(UserAttemptCreateDto userAttemptCreateDto)
        {
            var gameAttempt = await _gameSetupService.CreateUserAttempt(userAttemptCreateDto);
            if (!gameAttempt.IsSuccess)
            {
                return BadRequest(gameAttempt);
            }
            return Ok(gameAttempt);
        }
        #endregion

        #region GAME SETUP

        [HttpPost]
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

        [HttpGet("{id:int}", Name = "GetGameSetupByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<GameSetupDto>>> GetGameSetupByIdAsync(int id)
        {
            var gameSetup = await _gameSetupService.GetGameSetupById(id);
            if (!gameSetup.IsSuccess)
            {
                return BadRequest(gameSetup);
            }
            return Ok(gameSetup);
        }      
        
        [HttpGet("{id:guid}", Name = "GetGameSetupsByUserIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<List<GameSetupDto>>>> GetGameSetupsByUserIdAsync(Guid id)
        {
            var gameSetups = await _gameSetupService.GetGameSetupsByUserId(id);
            if (!gameSetups.IsSuccess)
            {
                return BadRequest(gameSetups);
            }
            return Ok(gameSetups);
        }
       
        #endregion
    }
}
