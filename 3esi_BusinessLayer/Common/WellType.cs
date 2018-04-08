using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esi_BusinessLayer.Common
{
    public enum WellType
    {
        [Description("Vertical")]
        Vertical = 0,
        [Description("Slanted")]
        Slanted,
        [Description("Horizontal")]
        Horizontal
    };

    
}
