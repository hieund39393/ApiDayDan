using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVN.Core.Models.Base
{
    public class BaseUser : BaseEntity
    {
        [StringLength(255)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string TenDangNhap { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string DienThoai { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string MatKhau { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string NgaySinh { get; set; }
        
        public bool HoatDong { get; set; } = true;                
    }
}
