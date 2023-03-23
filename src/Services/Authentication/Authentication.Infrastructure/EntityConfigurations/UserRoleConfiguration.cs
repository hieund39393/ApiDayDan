using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class UserRoleConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("AUTH_UserRoles");
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.RoleId);
            builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).IsRequired();
            ConfigureBase(builder);
        }
    }
}
