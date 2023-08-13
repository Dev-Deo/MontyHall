
namespace Shared.DTO
{
    public class GameResultSummeryDto
    {
        public int GameSetupId { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
        public string FirstChoice { get; set; }
        public string OpenedDoorNo { get; set; }
        public string SecondChoice { get; set; }
        public string WinStatus { get; set; }
    }
}
