using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class StateProjections
    {
        public static StateVm ToVm(this State state) => new StateVm
        {
            Initials = state.Initials,
            Name = state.Name
        };
        public static IQueryable<StateVm> ToVm(this IQueryable<State> query) 
            => query.Select(state 
                => new StateVm {
                    Initials = state.Initials,
                    Name = state.Name
                });
        public static IEnumerable<StateVm> ToVm(this IEnumerable<State> query)
            => query.Select(state
                => new StateVm
                {
                    Initials = state.Initials,
                    Name = state.Name
                });
    }
}