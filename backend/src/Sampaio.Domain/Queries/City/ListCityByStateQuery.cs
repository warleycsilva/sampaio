using System;
using System.Collections.Generic;
using MediatR;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.City
{
    public class ListCityByStateQuery : IRequest<IEnumerable<CityVm>>
    {
        public string StateId  { get; set; }
    }
}