using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class UserLoginConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("AUTH_UserLogins");
            builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });
            builder.HasOne(x => x.User).WithMany(x => x.UserLogins).HasForeignKey(x => x.UserId).IsRequired();
            builder.Property(x => x.LoginProvider).IsRequired().HasMaxLength(10);
            builder.Property(x => x.ProviderKey).IsRequired().HasMaxLength(10);
            builder.Property(x => x.ProviderDisplayName).HasMaxLength(200);
            ConfigureBase(builder);
        }
    }
}
