using System.Collections.Generic;

namespace EVN.Core.Interfaces.Offices
{
    public interface IRow
    {
        List<ICell> Cells { get; set; }
    }
}
