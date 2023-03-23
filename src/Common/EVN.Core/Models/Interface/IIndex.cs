using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVN.Core.Models.Interface
{
    public interface IIndex
    {
        public int? Index { get; set; }
    }
}
