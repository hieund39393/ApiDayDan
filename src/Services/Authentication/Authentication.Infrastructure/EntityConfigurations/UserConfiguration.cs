using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class UserConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AUTH_Users");
            builder.HasKey(x => new { x.Id });
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique(false);
            builder.Property(x => x.UserName).HasMaxLength(56);
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.NameUnsigned).HasMaxLength(256);
            builder.HasOne(x => x.Unit).WithMany(x => x.Users).HasForeignKey(x => x.UnitId);
            builder.HasOne(x => x.Department).WithMany(x => x.Users).HasForeignKey(x => x.DepartmentId);
            builder.HasOne(x => x.Team).WithMany(x => x.Users).HasForeignKey(x => x.TeamId);
            builder.HasOne(x => x.Position).WithMany(x => x.Users).HasForeignKey(x => x.PositionId);

            ConfigureBase(builder);
        }
    }
}
