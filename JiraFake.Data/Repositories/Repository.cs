using JiraFake.Data.Context;
using JiraFake.Domain.DomainObjects;
using JiraFake.Domain.Interfaces.Common;
using JiraFake.Domain.Interfaces.Infra;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JiraFake.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {

        protected readonly JiraFakeContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(JiraFakeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Dispose()
        {

        }
    }
}
