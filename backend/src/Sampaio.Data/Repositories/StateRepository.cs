using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Repositories
{
    public class StateRepository : Repository<State>,IStateRepository
    {
        public StateRepository(DataContext context) : base(context)
        {
        }
    }
}