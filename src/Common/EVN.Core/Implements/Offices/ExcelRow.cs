using EVN.Core.Interfaces.Offices;
using System.Collections.Generic;

namespace EVN.Core.Implements.Offices
{
    public class ExcelRow : IRow
    {
        public ExcelRow()
        {
            Cells = new List<ICell>();
        }
        public List<ICell> Cells { get; set; }
    }
}
