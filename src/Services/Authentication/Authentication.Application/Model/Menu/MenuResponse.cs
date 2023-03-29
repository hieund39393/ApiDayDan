using EVN.Core.SeedWork;
using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication.Application.Model.Menu
{
    public class MenuResponse : BaseExtendEntities
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
