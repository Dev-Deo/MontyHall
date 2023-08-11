using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;

namespace Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponceDto<ApplicationUserDto>> CreateUser(ApplicationUserCreateDto applicationUserDto)
        {
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = applicationUserDto.FirstName,
                    LastName = applicationUserDto.LastName,
                    Email = applicationUserDto.Email,
                    UserName = applicationUserDto.Email,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, applicationUserDto.Password);
                return new ResponceDto<ApplicationUserDto>()
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Errors.FirstOrDefault()?.Description,
                    Data = new ApplicationUserDto
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Id = user.Id
                    }
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


        public async Task<ResponceDto<bool>> DeleteApplicationUser(Guid id)
        {
            try
            {
                var applicationUser = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == id);
                var result = await _userManager.DeleteAsync(applicationUser);
                return new ResponceDto<bool>()
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Errors.FirstOrDefault()?.Description,
                    Data = result.Succeeded
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<bool>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponceDto<List<ApplicationUserDto>>> GetUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.Select(s => new ApplicationUserDto
                {
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Id = s.Id,
                }).ToListAsync();
                return new ResponceDto<List<ApplicationUserDto>>
                {
                    Data = users,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {

                return new ResponceDto<List<ApplicationUserDto>>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponceDto<ApplicationUserDto>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userManager.Users.Select(s => new ApplicationUserDto
                {
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Id = s.Id,
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

        public async Task<ResponceDto<ApplicationUserDto>> UpdateApplicationUser(ApplicationUserUpdateDto applicationUserUpdateDto)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == applicationUserUpdateDto.Id);
                user.FirstName = applicationUserUpdateDto.FirstName;
                user.LastName = applicationUserUpdateDto.LastName;
                var result = await _userManager.UpdateAsync(user);
                return new ResponceDto<ApplicationUserDto>()
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Errors.FirstOrDefault()?.Description,
                    Data = new ApplicationUserDto
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Id = user.Id
                    }
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
    }
}
