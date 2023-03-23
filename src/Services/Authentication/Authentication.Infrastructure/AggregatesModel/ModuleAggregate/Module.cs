using EVN.Core.Models.Interface;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.ModuleAggregate
{
    public class Module : BaseEntity
    {
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public string Url { get; set; }
        public int NumberOrder { get; set; }
        public string Icon { get; set; }
        public Guid? ParentId { get; set; }
        public Module ModuleParent { get; set; }
        public ICollection<Module> ModuleChilds { get; set; }

        public ICollection<Role> Role { get; set; }

    }
}
