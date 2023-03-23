using EVN.Core.SeedWork;
using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Authentication.Application.Model.Page
{
    public class PageResponse : BaseExtendEntities
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string Url { get; set; }
        public int TrangThai { get; set; }
        public string TenPageCha { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid>? ListPageCon { get; set; }
        public List<string> ListTenPageCon { get; set; }

    }
}
