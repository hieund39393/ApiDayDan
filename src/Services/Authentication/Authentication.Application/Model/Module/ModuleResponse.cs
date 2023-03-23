using EVN.Core.SeedWork.ExtendEntities;
using System;

namespace Authentication.Application.Model.Module
{

    public class ModuleResponse
    {
        public Guid Id { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public string TenVietTat { get; set; }
        public string Url { get; set; }
        public int TrangThai { get; set; }
        public int NumberOrder { get; set; }
        public bool TopMenu { get; set; }
        public string Icon { get; set; }
    }
}
