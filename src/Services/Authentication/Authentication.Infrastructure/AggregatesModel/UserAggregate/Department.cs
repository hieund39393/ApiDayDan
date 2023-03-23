using EVN.Core.Models.Base;
using EVN.Core.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? Index { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
