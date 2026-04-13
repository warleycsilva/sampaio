using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Entities;

namespace Sampaio.Data.Maps
{
    public class PlanMap : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plans");

            builder.HasKey(x => x.Id);

            builder.MapUniqueIdentifier(x => x.Id, "Id");

            builder.MapDeleted();

            builder.MapCreatedAt();

            builder.MapVarcharMax(x => x.Name, true);

            builder.MapVarcharMax(x => x.Description, true);

            builder.MapDecimal(x => x.MonthValue, 18, 2);
        }
    }
}
