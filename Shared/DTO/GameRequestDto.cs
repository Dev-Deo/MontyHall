
namespace Shared.DTO
{
    public class GameRequestDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public int TotalGameRequests { get; set; }
    }
}
