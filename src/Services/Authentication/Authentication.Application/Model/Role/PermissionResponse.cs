using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.Role
{
    public class PermissionResponse
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public List<PermissionResponse> Children { get; set; }
    }
}
