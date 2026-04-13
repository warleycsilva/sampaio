using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class Debt : BaseEntity
    {
        public decimal Value { get; private set; }
        public Driver Driver { get; private set; }
        public Guid? DriverId { get; private set; }
        public EDebtStatus Status { get; private set; }
        public EDebtType Type { get; private set; }
        public Plan? Plan { get; private set; }
        public Guid? PlanId { get; private set; }
        
        public string? ExternalId { get; private set; }
        public string? QrCode { get; private set; }
        
        public string? QrCodeImage { get; private set; }

        public ICollection<DebtPayment> DebtPayments { get; private set; } = new List<DebtPayment>();
        public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

        public static Debt New(decimal value, 
            Guid? driverId, EDebtStatus status,
            EDebtType type, string? externalId= null,
            Guid? planId = null
            , string qrCode = null
            , string qrCodeImage = null
        ) => new Debt()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            DriverId = driverId,
            Value = value,
            Status = status,
            Type = type,
            ExternalId = externalId,
            PlanId = planId,
            QrCode = qrCode,
            QrCodeImage = qrCodeImage,
        };

        public void Update(decimal? value, Guid? driverId, EDebtStatus? status, EDebtType? type)
        {
            this.Value = value ?? Value;
            this.DriverId = driverId ?? DriverId;
            this.Value = value ?? Value;
            this.Status = status ?? Status;
            this.Type = type ?? Type;
        }

        public void Delete() => Deleted = true;

        public void SetPaid()
        {
            Status = EDebtStatus.Paid;
        }

        public void SetStatus(EDebtStatus eDebtStatus)
        {
            Status = eDebtStatus;
        }
    } 
}
