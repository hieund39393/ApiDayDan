using EVN.Core.SeedWork;
using System;

namespace Authentication.Application.Model.Module
{
    public class ModuleRequest
    {
        public Guid Id { get; set; }
        public string SearchTerm { get; set; }
    }
}
