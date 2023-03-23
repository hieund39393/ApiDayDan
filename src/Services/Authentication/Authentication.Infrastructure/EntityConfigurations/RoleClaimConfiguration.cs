using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class RoleClaimConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("AUTH_RoleClaims");
            builder.HasKey(x => new { x.Id });
            builder.HasOne(x => x.Role).WithMany(x => x.RoleClaims).HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property(x => x.ClaimType).HasMaxLength(50);
            builder.Property(x => x.ClaimValue).HasMaxLength(200);
            ConfigureBase(builder);
        }
    }
}
