using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class UserClaimConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("AUTH_UserClaims");
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.ClaimType).HasMaxLength(50);
            builder.Property(x => x.ClaimValue).HasMaxLength(200);
            builder.HasOne(x => x.User).WithMany(x => x.UserClaims).HasForeignKey(x => x.UserId).IsRequired();
            ConfigureBase(builder);
        }
    }
}
