using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.Role
{
    public class GetUserRoleResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string CMIS_CODE { get; set; }
        public bool Actived { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
