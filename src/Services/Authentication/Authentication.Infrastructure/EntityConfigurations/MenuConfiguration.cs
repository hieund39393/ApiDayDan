using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class MenuConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("AUTH_Menu");
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Code).HasMaxLength(10);
            builder.HasOne(x => x.Module).WithMany(x => x.Menus).HasForeignKey(x => x.ModuleId);
            ConfigureBase(builder);
        }
    }
}
