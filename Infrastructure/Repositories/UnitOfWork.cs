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
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void SaveAsync()
        {
            _db.SaveChangesAsync().GetAwaiter().GetResult();
        }

        //public async Task<IDbContextTransaction> BeginTransactionAsync()
        //{
        //    return await _db.Database.BeginTransactionAsync();
        //}

        //public async void CommitAsync(IDbContextTransaction transaction)
        //{
        //    await transaction.CommitAsync();
        //}

        //public async void RollbackAsync(IDbContextTransaction transaction)
        //{
        //    await transaction.RollbackAsync();
        //}
    }
}
