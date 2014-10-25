using System.Threading;
using System.Threading.Tasks;

namespace Generic.Core.Context
{
    public interface IMyContextAsync : IMyContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}