using EVN.Core.Models.Interface;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public class UserRole : IdentityUserRole<Guid>, IEntity, IDeletable
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
