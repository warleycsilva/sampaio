using System;
using MediatR;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Establishment
{
    public class EstablishmentByIdQuery: IRequest<EstablishmentVm>
    {
        public Guid Id { get; set; }
    }
}
