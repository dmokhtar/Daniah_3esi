using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    [DelimitedRecord(",")]
    class GroupRecord : IRecord
    {
        public String RecordType { get; set; }

        public string Name { get; set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public int Radius { get; set; }
    }
}
