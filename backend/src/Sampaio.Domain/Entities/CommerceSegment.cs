using System;
using System.Collections.Generic;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class CommerceSegment : BaseEntity
    {
        protected CommerceSegment()
        {
        }

        public string Name { get; private set; }

        public string Description { get; private set; }
        
        public ICollection<Commerce> Commerces { get; set; } = new List<Commerce>();

        public static CommerceSegment New(string name, string description)
        {
            return new CommerceSegment
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };
        }

        public CommerceSegment Update(string name, string description)
        {
            Name = name;
            Description = description;
            return this;
        }

        public void Delete() => Deleted = true;
    }
}