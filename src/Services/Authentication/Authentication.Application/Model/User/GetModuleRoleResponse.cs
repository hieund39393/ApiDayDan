using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication.Application.Model.User
{
    public class GetModuleRoleResponse
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        [Comment("Diễn giải")]
        public string Description { get; set; }
        public string ModuleName { get; set; }
    }
}
