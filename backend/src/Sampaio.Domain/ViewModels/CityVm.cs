using System;

namespace Sampaio.Domain.ViewModels
{
    public class CityVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public StateVm State { get; set; }
    }
}