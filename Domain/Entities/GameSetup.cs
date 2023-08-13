using Domain.Common;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class GameSetup:BaseEntity
    {
        public int GameRequestId { get; set; }
        [ForeignKey("GameRequestId")]
        public GameRequest GameRequest { get; set; }
        public int GameRequestNo { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
        public GameResult GameResult { get; set; }
    }
}
