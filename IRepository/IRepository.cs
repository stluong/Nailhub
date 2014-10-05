using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.MyState;
using Infrastructure.Repository.QueryableRepository;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : IObjectState
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertOrUpdateGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryableOperation<TEntity> Query(IQueryExpression<TEntity> queryObject);
        IQueryableOperation<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryableOperation<TEntity> Query();
        //IQueryable Queryable(ODataQueryOptions<TEntity> oDataQueryOptions);
        IQueryable<TEntity> Queryable();
        IRepository<T> GetRepository<T>() where T : class, IObjectState;
    }
}