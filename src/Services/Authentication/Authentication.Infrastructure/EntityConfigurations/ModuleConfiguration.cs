﻿using Authentication.Infrastructure.EF;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class ModuleConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("AUTH_Module");
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Code).HasMaxLength(10);
            ConfigureBase(builder);
        }
    }
}
