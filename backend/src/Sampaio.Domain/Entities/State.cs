using System.Collections.Generic;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class State 
    {
        public string Name { get; private set; }

        public string Initials { get; private set; }
        
        public ICollection<City> Cities { get; private set; } = new List<City>();
    }
}