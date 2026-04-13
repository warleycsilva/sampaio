using System.Threading.Tasks;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Logging;
using Sampaio.Shared.Persistence;

namespace Sampaio.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            _logger = Logging4NetFactory.Logger;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _context.Database.CurrentTransaction.Commit();
            }
            catch (System.Exception e)
            {
                RollbackTransaction();
                _logger.Error(e);
            }

        }

        public void RollbackTransaction()
        {
            try
            {
                _context.Database.CurrentTransaction.Rollback();
            }
            catch (System.Exception e)
            {
                _logger.Error(e);
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
