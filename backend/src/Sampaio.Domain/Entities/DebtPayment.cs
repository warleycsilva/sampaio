using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class DebtPayment : BaseEntity
    {
        public Debt Debt { get; private set; }
        public Guid? DebtId { get; private set; }
        public EDebtPaymentStatus Status { get; private set; }
        public string ExternalCode { get; private set; }

        public static DebtPayment New(Guid? debtId, EDebtPaymentStatus status, string externalCode) => new DebtPayment()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            DebtId = debtId,
            Status = status,
            ExternalCode = externalCode,
        };

        public void Update(Guid? debtId, EDebtPaymentStatus? status, string? externalCode)
        {
            this.DebtId = debtId ?? DebtId;
            this.Status = status ?? Status;
            this.ExternalCode = externalCode ?? ExternalCode;
        }

        public void Delete() => Deleted = true;
    }
}
