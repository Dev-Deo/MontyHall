
namespace Shared.DTO
{
    public class GameSetupUpdateDto
    {
        public int Id { get; set; }
        public int GameRequestId { get; set; }
        public int GameRequestNo { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
    }
}
