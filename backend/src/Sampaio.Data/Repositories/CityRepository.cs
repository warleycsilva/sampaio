using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DataContext context) : base(context)
        {
        }
    }
}