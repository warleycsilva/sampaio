using System.Collections.Generic;
using MediatR;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.State
{
    public class ListStateQuery : IRequest<IEnumerable<StateVm>>
    {
        
    }
}