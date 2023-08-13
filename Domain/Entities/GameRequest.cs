using Domain.Common;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class GameRequest : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int TotalGameRequests { get; set; }
        public List<GameSetup> GameSetups { get; set; }
    }
}
