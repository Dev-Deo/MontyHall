
namespace Shared.DTO
{
    public class GameResultDto
    {
        public int Id { get; set; }
        public int GameSetupId { get; set; }
        public GameSetupDto GameSetup { get; set; }
        public int FirstChoice { get; set; }
        public int SecondChoice { get; set; }
        public bool IsWin { get; set; }
    }
}
