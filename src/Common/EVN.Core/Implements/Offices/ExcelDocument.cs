using EVN.Core.Interfaces.Offices;
using System.Collections.Generic;

namespace EVN.Core.Implements.Offices
{
    public class ExcelDocument : IDocument
    {
        public ExcelDocument()
        {
            Sheets = new List<ISheet>();
        }
        public List<ISheet> Sheets { get; set; }
    }
}
