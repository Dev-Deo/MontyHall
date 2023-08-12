using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            GameRequest = new GameRequestRepository(_db);
            GameSetup = new GameSetupRepository(_db);
            GameResult = new GameResultRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IGameRequestRepository GameRequest { get; private set; }
        public IGameSetupRepository GameSetup { get; private set; }
        public IGameResultRepository GameResult { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void SaveAsync()
        {
            _db.SaveChangesAsync().GetAwaiter().GetResult();
        }

    }
}
