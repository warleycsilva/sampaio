using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Commerce
{
    public class GetCommerceByIdQuery : IRequest<CommerceVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}