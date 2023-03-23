using EVN.Core.Models.Base;
using EVN.Core.Models.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public class Team: BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
