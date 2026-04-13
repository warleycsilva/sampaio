using System.Threading.Tasks;

namespace Sampaio.Shared.Persistence
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        
        Task<int> SaveChangesAsync();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}