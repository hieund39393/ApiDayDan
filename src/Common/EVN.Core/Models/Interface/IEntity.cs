using System;

namespace EVN.Core.Models.Interface
{
    public interface IEntity
    {
        DateTime CreatedDate { get; set; }
        Guid? CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
