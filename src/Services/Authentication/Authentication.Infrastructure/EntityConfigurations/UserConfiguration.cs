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
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.NameUnsigned).HasMaxLength(256);
           
            ConfigureBase(builder);
        }
    }
}
