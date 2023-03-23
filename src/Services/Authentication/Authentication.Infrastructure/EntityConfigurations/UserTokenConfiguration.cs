using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class UserTokenConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("AUTH_UserTokens");
            builder.HasKey(x => new { x.Value });
            builder.HasOne(x => x.User).WithMany(x => x.UserTokens).HasForeignKey(x => x.UserId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.LoginProvider).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            ConfigureBase(builder);
        }

    }
}
