using JiraFake.Domain.DomainObjects;
using JiraFake.Domain.Interfaces.Common;
using System.Linq.Expressions;

namespace JiraFake.Domain.Interfaces.Infra
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
