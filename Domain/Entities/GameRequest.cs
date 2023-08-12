using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class GameRequest : BaseEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TotalGameRequests { get; set; }
    }
}
