using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Authentication.Application.Model.User
{
    public class UserResponse : BaseExtendEntities
    {
        public Guid Id { get; set; }
        public Guid? PositionId { get; set; }
        public string UnitName { get; set; }
        public string Name { get; set; }
        public string PositionName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Actived { get; set; }
        public List<Guid> RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAdminRole { get; set; }
        [Comment("Mã đơn vị")]
        public string MaDonVi { get; set; }
        [Comment("Chức vụ, vai trò")]
        public List<string> RoleName { get; set; }

    }
}
