using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.DTO;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ResponceDto<CurrentUserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                    return new ResponceDto<CurrentUserDto>()
                    {
                        IsSuccess = false,
                        Message = "User name or password is incorrect"
                    };

                var result = await _signInManager.CheckPasswordSignInAsync(
                    user,
                    loginDto.Password,
                    false
                );

                if (!result.Succeeded)
                    return new ResponceDto<CurrentUserDto>()
                    {
                        IsSuccess = false,
                        Message = "User name or password is incorrect"
                    };

                var token = _tokenService.CreateToken(user);
                if (!token.IsSuccess)
                {
                    return new ResponceDto<CurrentUserDto>()
                    {
                        IsSuccess = false,
                        Message = token.Message
                    };
                }

                return new ResponceDto<CurrentUserDto>()
                {
                    IsSuccess = true,
                    Data = new CurrentUserDto()
                    {
                        Email = user.Email,
                        Token = token.Data.Token,
                        DisplayName = user.FirstName + " " + user.LastName,
                        UserId = user.Id,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<CurrentUserDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
