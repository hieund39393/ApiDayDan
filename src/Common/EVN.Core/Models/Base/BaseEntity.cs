using EVN.Core.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVN.Core.Models.Base
{
    public class BaseEntity : IEntity, IDeletable, IAuditEntity
    {
        [Comment("Id bảng, khóa chính")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int EntityId { get; set; }

        [Comment("Cờ xóa")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Comment("Mã người tạo")]        
        public Guid? CreatedBy { get; set; }

        [Comment("Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;

        [Comment("Mã người cập nhật")]        
        public Guid? UpdatedBy { get; set; }
    }
}
