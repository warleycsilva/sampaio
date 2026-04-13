using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Driver
{
    public class GetDriverByIdQuery : IRequest<DriverVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
