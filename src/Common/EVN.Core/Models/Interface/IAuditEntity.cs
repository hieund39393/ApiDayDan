using System;

namespace EVN.Core.Models.Interface
{
    public interface IAuditEntity : IEntity
    {
        public Guid Id { get; set; }
        public int EntityId { get; set; }
    }
}
