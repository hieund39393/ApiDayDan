using EVN.Core.SeedWork;
using System;

namespace Authentication.Application.Model.Page
{
    public class PageRequest : PagingQuery
    {
        public Guid ModuleId { get; set; }
    }
}
