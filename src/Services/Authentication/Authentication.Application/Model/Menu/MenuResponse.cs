using EVN.Core.SeedWork;
using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication.Application.Model.Menu
{
    public class MenuResponse : BaseExtendEntities
    {
        public Guid Id { get; set; }
        public string TenMenu { get; set; }
        public Guid MenuCha { get; set; }
        public string TenMenuCha { get; set; }
        public Guid ModuleId { get; set; }
        public string TenModule { get; set; }
        public string Url { get; set; }
        public bool LeftMenu { get; set; }
        public int TrangThai { get; set; } 
    }
}
