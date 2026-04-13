using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public Guid Id { get; set; }
        public decimal MonthValue { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Driver> Drivers { get; set; } = new List<Driver>();
        public IEnumerable<Debt> Debts { get; set; } = new List<Debt>();

        public static Plan New(string name, string description, decimal monthValue) => new Plan()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            Name = name,
            Description = description,
            MonthValue = monthValue
        };

        public void Update(string? name = null, string? description = null, decimal? monthValue = null)
        {
            this.Name = name ?? Name;
            this.Description = description ?? Description;
            this.MonthValue = monthValue ?? MonthValue;
        }

        public void Delete() => Deleted = true;
    }
}
