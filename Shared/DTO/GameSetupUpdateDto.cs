
namespace Shared.DTO
{
    public class GameSetupUpdateDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int AttemptNo { get; set; }
        public string? FirstDoor { get; set; }
        public string? SecondDoor { get; set; }
        public string? ThirdDoor { get; set; }
    }
}
