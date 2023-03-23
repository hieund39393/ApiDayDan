using EVN.Core.Models.Interface;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public class UserToken : IdentityUserToken<Guid>, IEntity, IDeletable
    {
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User User { get; set; }

    }
}
