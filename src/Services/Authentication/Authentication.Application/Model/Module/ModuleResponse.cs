using EVN.Core.SeedWork.ExtendEntities;
using System;

namespace Authentication.Application.Model.Module
{

    public class ModuleResponse
    {
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public string Url { get; set; }
        public int NumberOrder { get; set; }
        public string Icon { get; set; }
        
        public List<ModuleResponse> ModuleChild { get; set; }
    }
}
