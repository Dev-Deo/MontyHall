using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // login
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<CurrentUserDto>>> Login([FromBody] LoginDto loginDto)
        {
            var result = await _accountService.Login(loginDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
