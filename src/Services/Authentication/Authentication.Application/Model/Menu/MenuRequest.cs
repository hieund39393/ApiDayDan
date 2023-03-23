using EVN.Core.SeedWork;
using System;

namespace Authentication.Application.Model.Menu
{
    public class MenuRequest : PagingQuery
    {
        public Guid ModuleId { get; set; }
    }
}
