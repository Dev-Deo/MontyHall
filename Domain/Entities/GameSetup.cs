using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class GameSetup:BaseEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int AttemptNo { get; set; }
        public string FirstDoor { get; set; }
        public string SecondDoor { get; set; }
        public string ThirdDoor { get; set; }
    }
}
