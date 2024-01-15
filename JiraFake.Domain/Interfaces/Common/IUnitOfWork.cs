namespace JiraFake.Domain.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
