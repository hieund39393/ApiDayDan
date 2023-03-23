using System.Collections.Generic;

namespace EVN.Core.Interfaces.Offices
{
    public interface IDocument
    {
        List<ISheet> Sheets { get; set; }
    }
}
