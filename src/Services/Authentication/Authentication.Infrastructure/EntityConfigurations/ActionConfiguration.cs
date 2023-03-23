using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.ActionsAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<Actions>
    {
        public void Configure(EntityTypeBuilder<Actions> builder)
        {
            builder.ToTable("AUTH_Action");
            builder.Property(x => x.ModuleName).HasMaxLength(250);
            builder.Property(x => x.ModuleCode).HasMaxLength(10);
            builder.Property(x => x.ActionName).HasMaxLength(50);
            builder.Property(x => x.ActionCode).HasMaxLength(10);
        }
    }
}
