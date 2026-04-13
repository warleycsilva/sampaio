using System;
using System.Collections.Generic;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Entities
{
    public class Driver : BaseEntity
    {
        protected Driver()
        {
        }
        public User User { get; private set; }
        public Identification Identification { get; private set; }
        

        public string CnhNumber { get; private set; }
        public DateTime? Birthdate { get; private set; }

        public Guid? PlanId { get; private set; }
        public Plan Plan { get; private set; }
        
        public DateTime? PlanStartDate { get; private set; }

        
        public ICollection<Sale> Sales { get; private set; } = new List<Sale>();
        public ICollection<Debt> Debts { get; private set; } = new List<Debt>();
        public IEnumerable<Cart> Carts { get; set; } = new List<Cart>();

        public static Driver New(Identification identification = null, string? cnhNumber = null) => new Driver()
        {
            Identification = identification,
            CnhNumber = cnhNumber,
            CreatedAt = DateTime.UtcNow,
        };

        public Driver Update(Identification identification)
        {
            Identification = identification;
            return this;
        }

        public void Delete() => Deleted = true;
      
        public void SetPlan(Guid? planId)
        {
            PlanId = planId;
            PlanStartDate = DateTime.UtcNow;
        }
    }
}