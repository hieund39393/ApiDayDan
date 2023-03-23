using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Authentication.Application.Model.User
{
    public class UserResponse : BaseExtendEntities
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string CMIS_CODE { get; set; }
        public bool Actived { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAdminRole { get; set; }
        [Comment("Mã đơn vị")]
        public string MaDonVi { get; set; }
        [Comment("Chức vụ, vai trò")]
        public List<string> RoleName { get; set; }

    }
}
