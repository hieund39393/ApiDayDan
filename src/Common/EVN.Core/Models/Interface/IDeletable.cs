using Microsoft.EntityFrameworkCore;

namespace EVN.Core.Models.Interface
{
    public interface IDeletable : IEntity
    {
        [Comment("Cờ xóa")]
        bool IsDeleted { get; set; }
    }
}
