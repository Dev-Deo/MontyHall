
namespace Shared.DTO
{
    public class GameSetupDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public int AttemptNo { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
    }
}
