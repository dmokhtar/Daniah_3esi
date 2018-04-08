using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esi_BusinessLayer.Abstraction
{
    public interface IRecord
    {
        String RecordType { get; set; }

        string Name { get; set; }
    }
}
