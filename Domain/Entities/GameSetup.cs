using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class GameSetup:BaseEntity
    {
        public int GameRequestId { get; set; }
        public GameRequest GameRequest { get; set; }
        public int GameRequestNo { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
    }
}
