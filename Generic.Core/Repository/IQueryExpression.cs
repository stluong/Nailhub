using System;
using System.Linq.Expressions;

namespace Generic.Core.Repository
{
    public interface IQueryExpression<TEntity>
    {
        Expression<Func<TEntity, bool>> Query();
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> And(IQueryExpression<TEntity> queryObject);
        Expression<Func<TEntity, bool>> Or(IQueryExpression<TEntity> queryObject);
    }
}