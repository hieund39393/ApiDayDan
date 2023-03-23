using EVN.Core.Models.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.EF.EntityConfigurations
{
    /// <summary>
    /// Common Property Configuration
    /// </summary>
    public class CommonPropertyConfiguration
    {
        /// <summary>
        /// Configure Base
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        protected static void ConfigureBase<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity, IDeletable
        {
            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.CreatedBy).HasMaxLength(36);

            builder.Property(e => e.UpdatedBy).HasMaxLength(36);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
