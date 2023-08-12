using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GameRequestRepository : Repository<GameRequest>, IGameRequestRepository
    {
        private readonly ApplicationDbContext _db;
        public GameRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
