using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.PermissionAggregate
{
    public class Permission : BaseEntity
    {
        public Permission() { }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
