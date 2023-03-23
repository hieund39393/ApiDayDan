using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.User
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UnitId { get; set; }
        public string TenDonVi { get; set; }
        public string TenPhongBan { get; set; }
        public string TenToDoi { get; set; }
        public int Position { get; set; }
        public string Avatar { get; set; }
        public DateTime? Dob { get; set; }
        public List<string> RoleNames { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsActived { get; set; }
    }
}
