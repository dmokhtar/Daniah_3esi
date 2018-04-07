using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esi_BusinessLayer.Rules
{
    class GroupDetails : GroupRecord
    {
        List<WellDetails> WellDetailsList { get; set; }

        double Area { get; set; }
    }
}
