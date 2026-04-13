using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sampaio.Shared.Extensions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class DebtProjections
    {
        public static DebtVm ToVm(this Debt debt) => new DebtVm
        {
            Id = debt.Id,
            Value = debt.Value,
            Driver = debt?.Driver?.ToVm(),
            Status = debt.Status,
            Type = debt.Type,
            StatusDescription = debt.Status.Description(),
            TypeDescription = debt.Type.Description(),
            Plan = debt.Plan != null ? debt.Plan.ToVm() : null,
            QrCode = debt.QrCode,
            QrCodeImage = debt.QrCodeImage,
        };

        public static IQueryable<DebtVm> ToVm(this IQueryable<Debt> query) =>
            query.Select(debt => new DebtVm
            {
                Id = debt.Id,
                Value = debt.Value,
                Driver = debt.Driver != null ? debt.Driver.ToVm() : null,
                Status = debt.Status,
                Type = debt.Type,
                StatusDescription = debt.Status.Description(),
                TypeDescription = debt.Type.Description(),
                Plan = debt.Plan != null ? debt.Plan.ToVm() : null,
                QrCode = debt.QrCode,
                QrCodeImage = debt.QrCodeImage,
            });

        public static IEnumerable<DebtVm> ToVm(this IEnumerable<Debt> query)
        {
            return query.Select(debt => new DebtVm
            {
                Id = debt.Id,
                Value = debt.Value,
                Driver = debt?.Driver?.ToVm(),
                Status = debt.Status,
                Type = debt.Type,
                StatusDescription = debt.Status.Description(),
                TypeDescription = debt.Type.Description(),
                Plan = debt.Plan != null ? debt.Plan.ToVm() : null,
                QrCode = debt.QrCode,
                QrCodeImage = debt.QrCodeImage,
            });
        }
    }
}
