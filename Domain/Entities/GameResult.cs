using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class GameResult:BaseEntity
    {
        public int GameSetupId { get; set; }
        [ForeignKey("GameSetupId")]
        public GameSetup GameSetup { get; set; }
        public int FirstChoice { get; set; }
        public int OpenedDoorNo { get; set; }
        public int SecondChoice { get; set; }
        public bool IsWin { get; set; }

    }
}
