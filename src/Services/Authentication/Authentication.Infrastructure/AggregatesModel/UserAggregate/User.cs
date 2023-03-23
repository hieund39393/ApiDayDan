using EVN.Core.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Infrastructure.AggregatesModel.UserAggregate
{
    public class User : IdentityUser<Guid>, IEntity, IDeletable, IName, IIndex
    {
        public User() { }
        /// <summary>
        /// Tên người dùng
        /// </summary>
        [Comment("Tên người dùng")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Tên người dùng không dấu
        /// </summary>
        [Comment("Tên người dùng không dấu")]
        [StringLength(100)]
        public string NameUnsigned { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        [Comment("Ảnh đại diện")]
        [StringLength(256)]
        public string Avatar { get; set; }

        /// <summary>
        /// Cờ superadmin
        /// </summary>
        [Comment("Cờ superadmin")]
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// Cờ kích hoạt
        /// </summary>
        [Comment("Cờ kích hoạt")]
        public bool Actived { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Comment("Ngày sinh")]
        public DateTime? Dob { get; set; }

        public bool SSO { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Mã nhân viên CMIS
        /// </summary>
        [StringLength(50)]
        public string CMIS_CODE { get; set; }

        public Unit Unit { get; set; }
        public Department Department { get; set; }
        public Team Team { get; set; }
        public Role Role { get; set; }
        public int Position { get; set; }
        public int? Gender { get; set; }  // Giới tính 1=Nam, 2=Nữ

        public int? Index { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserClaim> UserClaims { get; set; }
        public ICollection<UserLogin> UserLogins { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }

        /// <summary>
        /// Get Permissions
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public string GetPermissions(string claimType)
        {
            var permissionClaims = GetPermissionClaims(claimType);
            if (permissionClaims == null) return null;
            return string.Join(',', GetPermissionClaims(claimType));
        }

        /// <summary>
        /// Get Permission Claims
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public List<string> GetPermissionClaims(string claimType)
        {
            if (UserRoles == null) return null;
            var claims = UserRoles.Select(x => x.Role).SelectMany(x => x.RoleClaims)
                .Where(x => x.ClaimType == claimType)
                .Select(x => x.ClaimValue)
                .Distinct().ToList();
            return claims;
        }

        /// <summary>
        /// Get Permission Data
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public string GetPermissionData(string claimType)
        {
            if (UserClaims == null) return null;
            var claims = UserClaims
                .Where(x => x.ClaimType == claimType)
                .Select(x => x.ClaimValue)
                .Distinct().ToList();
            return string.Join(',', claims);
        }
    }
}
