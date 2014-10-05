using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.MyState;

namespace Infrastructure.Repository.QueryableRepository
{
    public interface IQueryableOperation<TEntity> where TEntity : IObjectState
    {
        IQueryableOperation<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        IQueryableOperation<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryableOperation<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount);
        IEnumerable<TEntity> Get();
        IEnumerable<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector = null);
        Task<IEnumerable<TEntity>> GetAsync();
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}