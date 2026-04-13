using System;
using System.Collections.Generic;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Entities
{
    public class City
    {
        public Guid Id { get; protected set; }

        public string Name { get; private set; }

        public State State { get; private set; }

        public string StateId { get; private set; }
    }
}
