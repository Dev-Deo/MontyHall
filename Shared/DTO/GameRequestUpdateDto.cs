
namespace Shared.DTO
{
    public class GameRequestUpdateDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TotalGameRequests { get; set; }
    }
}
