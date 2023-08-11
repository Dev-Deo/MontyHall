namespace Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        void SaveAsync();
        //Task<IDbContextTransaction> BeginTransactionAsync();
        //void CommitAsync(IDbContextTransaction transaction);
        //void RollbackAsync(IDbContextTransaction transaction);
    }
}
