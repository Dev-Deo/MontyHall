namespace Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        IGameRequestRepository GameRequest { get; }
        IGameSetupRepository GameSetup { get; }
        IGameResultRepository GameResult { get; }
        void SaveAsync();
    }
}
