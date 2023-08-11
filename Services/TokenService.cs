using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }
        public ResponceDto<TokenDto> CreateToken(ApplicationUser user, TokenDto tokenDto = null)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                };

                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(3),
                    SigningCredentials = creds,
                    Issuer = _config["Token:Issuer"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenResult = tokenHandler.WriteToken(token);
                if (tokenResult is null)
                {
                    return new ResponceDto<TokenDto>()
                    {
                        IsSuccess = false,
                        Message = "Token was not created"
                    };
                }

                return new ResponceDto<TokenDto>()
                {
                    IsSuccess = true,
                    Data = new TokenDto()
                    {
                        Token = tokenResult,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponceDto<TokenDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
