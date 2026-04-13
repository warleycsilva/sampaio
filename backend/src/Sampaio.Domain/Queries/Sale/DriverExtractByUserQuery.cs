using Sampaio.Shared.Security;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Sale
{
    public class DriverExtractByUserQuery : IRequest<DriverExtractVm>
    {

        public SaleFilter Filter { get; set; }
    }
}