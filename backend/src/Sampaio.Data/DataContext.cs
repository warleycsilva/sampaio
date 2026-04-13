using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sampaio.Data.Maps;

namespace Sampaio.Data
{
    public class DataContext : DbContext
    {
        public DbContext Context { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
            Context = this;
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfigurationsFromAssembly(typeof(UserMap).GetTypeInfo().Assembly);
        }
    }
}