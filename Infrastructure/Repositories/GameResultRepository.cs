using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GameResultRepository : Repository<GameResult>, IGameResultRepository
    {
        private readonly ApplicationDbContext _db;
        public GameResultRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
