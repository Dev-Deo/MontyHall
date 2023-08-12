
namespace Shared.DTO
{
    public class GameSetupDto
    {
        public int Id { get; set; }
        public int GameRequestId { get; set; }
        public GameRequestDto GameRequest { get; set; }
        public int GameRequestNo { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
    }
}
