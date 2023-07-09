using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.PermissionAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.MenuAggregate
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid ModuleId { get; set; }
        public Module Module { get; set; }

        public Guid? ParenId { get; set; }
        public ICollection<Menu> Menus { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
