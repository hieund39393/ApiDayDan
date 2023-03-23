using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class RoleConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("AUTH_Roles");
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
            builder.Property(o => o.NormalizedName).HasMaxLength(256);
            builder.Property(o => o.ConcurrencyStamp).HasMaxLength(256);
            builder.HasIndex(x => x.NormalizedName).IsUnique();
            ConfigureBase(builder);
        }
    }
}
