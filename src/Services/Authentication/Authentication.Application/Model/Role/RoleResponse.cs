using EVN.Core.SeedWork.ExtendEntities;
using System;

namespace Authentication.Application.Model.Role
{
    public class RoleResponse : BaseExtendEntities
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
