using System;
using Infrastructure.MyState;

namespace Infrastructure.MyContext
{
    public interface IMyContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}