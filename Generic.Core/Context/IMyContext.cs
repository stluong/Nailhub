using System;
using Generic.Core.Repository;

namespace Generic.Core.Context
{
    public interface IMyContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}