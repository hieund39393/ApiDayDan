using EVN.Core.Models.Interface;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public class Role : IdentityRole<Guid>, IEntity, IDeletable
    {
        public Module Module { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }
        public UserPosition? Type { get; set; }

    }
}
