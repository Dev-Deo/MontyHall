using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GameSetupRepository : Repository<GameSetup>, IGameSetupRepository
    {
        private readonly ApplicationDbContext _db;
        public GameSetupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
