using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.MenuAggregate
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public Guid ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
