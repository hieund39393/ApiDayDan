using EVN.Core.Models.Base;
using EVN.Core.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.ActionsAggregate
{
    public class Actions : BaseEntity
    {
        /// <summary>
        /// Mã trang
        /// </summary>
        public string ModuleCode { get; set; }
        /// <summary>
        /// Tên trang
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// Mã thao tác
        /// </summary>
        public string ActionCode { get; set; }
        /// <summary>
        /// Tên thao tác
        /// </summary>
        public string ActionName { get; set; }

    }
}
