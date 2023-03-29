using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Infrastructure.AggregatesModel.PositionAggregate
{
    [Table("AUTH_Position")]
    public class Position : BaseEntity
    {
        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
