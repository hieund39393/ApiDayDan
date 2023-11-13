using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.ActionsAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class VanBanThongBaoConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<VanBanThongBao>
    {
        public void Configure(EntityTypeBuilder<VanBanThongBao> builder)
        {
            builder.ToTable("VanBanThongBao");
            ConfigureBase(builder);
        }
    }
}
