using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace API.Controllers
{
    public class UserMangerController : ApiControllerBase
    {
        private readonly IUserManagerService _userMangerService;

        public UserMangerController(IUserManagerService userMangerService)
        {
            _userMangerService = userMangerService;
        }

        //get users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<List<ApplicationUserDto>>>> GetUsersAsync()
        {
            var users = await _userMangerService.GetUsersAsync();
            if (!users.IsSuccess)
            {
                return BadRequest(users);
            }
            return Ok(users);
        }

        // get user by userid
        [HttpGet("{id:guid}", Name = "GetUserByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<ApplicationUserDto>>> GetUserByIdAsync(Guid id)
        {
            var user = await _userMangerService.GetUserByIdAsync(id);
            if (!user.IsSuccess)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
        //create new user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<ApplicationUserDto>>> CreateUser(ApplicationUserCreateDto applicationUserDto)
        {
            var result = await _userMangerService.CreateUser(applicationUserDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // update user info
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponceDto<ApplicationUserDto>>> UpdateUser(ApplicationUserUpdateDto applicationUserUpdateDto)
        {
            var result = await _userMangerService.UpdateApplicationUser(applicationUserUpdateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
