using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication.Infrastructure.AggregatesModel.LogAggregate
{
    public class SystemLog
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [Comment("Chi tiết lỗi")]
        public string Message { get; set; }

        /// <summary>
        /// MessageTemplate
        /// </summary>
        [Comment("Chi tiết lỗi")]
        public string MessageTemplate { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        [Comment("Mức độ lỗi")]
        public string Level { get; set; }

        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Exception
        /// </summary>
        [Comment("Ngoại lệ")]
        public string Exception { get; set; }
        public string LogEvent { get; set; }
        [Comment("Tên user")]
        public string UserName { get; set; }
        [Comment("Địa chỉ IP")]
        public string IP { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        public string Properties { get; set; }
    }
}
