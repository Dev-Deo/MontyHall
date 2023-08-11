
namespace Shared.DTO
{
    public class CurrentUserDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }

}
