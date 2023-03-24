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
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;

namespace Authentication.Infrastructure.AggregatesModel.ModuleAggregate
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public ICollection<Menu> Menus { get; set; }

    }
}
