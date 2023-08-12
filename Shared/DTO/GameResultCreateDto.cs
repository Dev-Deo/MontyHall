
namespace Shared.DTO
{
    public class GameResultCreateDto
    {
        public int GameSetupId { get; set; }
        public string? FirstChoice { get; set; }
        public string? SecondChoice { get; set; }
        public bool? IsWin { get; set; } = false;
    }
}
