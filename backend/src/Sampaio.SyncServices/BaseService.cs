using System;
using System.Threading.Tasks;
using Sampaio.Shared.Persistence;

namespace Sampaio.SyncServices
{
    public class BaseService

    {
        private readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                {
                    if (await _uow.SaveChangesAsync() > 0) return true;
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}