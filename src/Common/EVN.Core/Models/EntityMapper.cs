using EVN.Core.Extensions;
using EVN.Core.Models.Interface;
using EVN.Core.Utility;
using System;

namespace EVN.Core.Models
{
    public static class EntityMapper
    {
        public static IEntity ToNewLogEntity(IAuditEntity entity, Guid? updatedBy)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = updatedBy;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = updatedBy;

            return entity;
        }
        public static IEntity ToUpdateLogEntity(IAuditEntity entity, Guid? updatedBy)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = updatedBy;
            return entity;
        }
        public static IEntity ToDeleteLogEntity(IDeletable entity, bool isDelete = true)
        {
            entity.IsDeleted = isDelete;
            return entity;
        }
    }
}
