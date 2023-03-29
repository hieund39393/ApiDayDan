using Authentication.Infrastructure.AggregatesModel.PermissionAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class PermissionConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("AUTH_Permission");
            builder.HasIndex(x => x.Code);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.HasOne(x => x.Menu).WithMany(x => x.Permissions).HasForeignKey(x => x.MenuId);

            ConfigureBase(builder);
        }
    }
}
