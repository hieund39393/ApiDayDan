using Authentication.Infrastructure.AggregatesModel.LogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    public class SystemLogConfiguration : IEntityTypeConfiguration<SystemLog>
    {
        public static string TableName = "SystemLogs";
        public void Configure(EntityTypeBuilder<SystemLog> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Message).HasColumnName("Message");
            builder.Property(x => x.MessageTemplate).HasColumnName("MessageTemplate");
            builder.Property(x => x.Level).HasColumnName("Level");
            builder.Property(x => x.TimeStamp).HasColumnName("TimeStamp").IsRequired();
            builder.Property(x => x.Exception).HasColumnName("Exception");
            builder.Property(x => x.LogEvent).HasColumnName("LogEvent").HasMaxLength(200);
            builder.Property(x => x.IP).HasColumnName("IP").HasMaxLength(200);
            builder.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(200);
            builder.Property(x => x.Properties).HasColumnName("Properties").HasColumnType("xml");
        }
    }
}
