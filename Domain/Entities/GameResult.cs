using Domain.Common;
using Domain.Entities;

namespace Shared.DTO
{
    public class GameResult:BaseEntity
    {
        public int GameSetupId { get; set; }
        public GameSetup GameSetup { get; set; }
        public string FirstChoice { get; set; }
        public string SecondChoice { get; set; }
        public bool IsWin { get; set; } 
    }
}
