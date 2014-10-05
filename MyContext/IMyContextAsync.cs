using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MyContext
{
    public interface IMyContextAsync : IMyContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}