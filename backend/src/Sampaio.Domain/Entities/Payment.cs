using System;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Debt Debt { get; private set; }
        public Guid DebtId { get; private set; }
        public EPaymentStatus Status { get; private set; }

        public static Payment New(Guid debtId, EPaymentStatus status) => new Payment()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            DebtId = debtId,
            Status = status,
        };

        public void Update(Guid? debtId, EPaymentStatus? status)
        {
            this.DebtId = debtId ?? DebtId;
            this.Status = status ?? Status;
        }
        
        public void Delete() => Deleted = true;
    }
}